using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Oak.TaskScheduler.Services
{
    public class Scheduler : BackgroundService, IScheduler
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<Scheduler> logger;
        private readonly IOptions<SchedulerOptions> options;

        public Scheduler(
            IServiceProvider serviceProvider,
            ILogger<Scheduler> logger,
            IOptions<SchedulerOptions> options)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
            this.options = options;
        }

        public Action OnIteration  { get; set; } = null;
        public Action OnIterationScopesLimit { get; set; } = null;


        private List<Task> activeTasks { get; set; } = new List<Task>();
        private bool stop = false;
        private CancellationToken stoppingToken;

        public async Task Start(CancellationToken token)
        {
            this.stop = false;
            this.stoppingToken = token;

            while (!this.stoppingToken.IsCancellationRequested && !this.stop)
            {
                var startIter = DateTime.UtcNow;
                this.OnIteration?.Invoke();

                if (this.activeTasks.Count > this.options.Value.IterationScopeLimit)
                {
                    // Over concurrent execution limit, this stops things getting too out of hand.
                    // The execution limit should be set based on the use case.
                    
                    this.OnIterationScopesLimit?.Invoke();

                    this.logger.LogCritical($"Scheduler exceeeded loop execution limit of {this.options.Value.IterationScopeLimit}");

                    await this.endIteration(startIter);
                    continue;
                }

                try
                {
                    // Create a new service scope (allows use of transient & scoped services)
                    // Execute Tasks & add to active

                    var scope = this.serviceProvider.CreateScope();
                    var tasksScope = scope.ServiceProvider.GetService<ITasksScope>();
                    this.activeTasks.Add(tasksScope.Handle(scope, this.stoppingToken));
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex.ToString());
                }

                await this.endIteration(startIter);
            }
        }

        public void Stop()
        {
            this.stop = true;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("Scheduler started at: {time}", DateTime.UtcNow);
            await this.Start(stoppingToken);
        }

        private async Task endIteration(DateTime start)
        {
            var end = DateTime.UtcNow;
            int delayMs = this.options.Value.IterationDelayMs;
            if (this.options.Value.IncludeRuntimeInDelay)
            {
                var span = (end - start).Milliseconds;
                delayMs -= span;
            }

            if (delayMs <= 0)
                return;

            await Task.Delay(delayMs, this.stoppingToken);

            this.cleanActiveTasks();
        }

        private void cleanActiveTasks()
        {
            // Remove completed tasks from active set

            var completed = this.activeTasks.Where(t => t.IsCompleted).ToList();
            foreach (var c in completed)
                this.activeTasks.Remove(c);
        }
    }
}
