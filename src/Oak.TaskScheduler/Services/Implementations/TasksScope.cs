using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Oak.TaskScheduler.Services
{
    public class TasksScope : ITasksScope
    {
        private readonly ILogger<TasksScope> logger;
        private readonly ITaskHandler taskHandler;
        private readonly IEnumerable<IScheduledTask> tasks;

        private Guid guid { get; set; }

        public TasksScope(
            ILogger<TasksScope> logger,
            ITaskHandler taskHandler,
            IEnumerable<IScheduledTask> tasks)
        {
            this.logger = logger;
            this.taskHandler = taskHandler;
            this.tasks = tasks;
            this.guid = Guid.NewGuid();
        }

        private List<Task> activeTasks = new List<Task>();

        public virtual async Task Handle(IServiceScope scope, CancellationToken token = default)
        {
            this.logger.LogInformation($"Task Set {this.guid.ToString()} Started");

            foreach (var task in tasks)
            {
                this.activeTasks.Add(this.taskHandler.ExecuteTask(task, token));
            }

            await Task.WhenAll(this.activeTasks);
            scope.Dispose();

            this.logger.LogInformation($"Task Set {this.guid.ToString()} Finished");

            return;
        }
    }
}
