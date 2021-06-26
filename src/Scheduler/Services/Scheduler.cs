using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Scheduler
{

    /// <summary>
    /// Scheduler background service. Handles asyncronous & concurrent execution of tasks 
    /// with a new service scope being created each passthrough.
    /// </summary>
    public class Scheduler : BackgroundService
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

        private List<Task> activeTasks { get; set; } = new List<Task>();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("Scheduler started at: {time}", DateTime.UtcNow);

            while (!stoppingToken.IsCancellationRequested)
            {
                if (this.activeTasks.Count > this.options.Value.IterationScopeLimit)
                {
                    // Over concurrent execution limit, this stops things getting too out of hand.
                    // The execution limit should be set based on the use case.

                    this.logger.LogCritical($"Scheduler exceeeded loop execution limit of {this.options.Value.IterationScopeLimit}");
                    await Task.Delay(this.options.Value.IterationDelayMs, stoppingToken);
                    this.cleanActiveTasks();
                    continue;
                }

                try 
                {
                    // Create a new service scope (allows use of transient & scoped services)
                    // Execute Tasks & add to active

                    var scope = this.serviceProvider.CreateScope();
                    var tasksScope = scope.ServiceProvider.GetService<TasksScope>();
                    this.activeTasks.Add(tasksScope.Handle(scope, stoppingToken));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.ToString());
                }

                await Task.Delay(this.options.Value.IterationDelayMs, stoppingToken);

                this.cleanActiveTasks();
            }
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
