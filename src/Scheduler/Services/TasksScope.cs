using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Scheduler
{
    /// <summary>
    /// Handles execution of a new task set in a new service scope
    /// </summary>
    public class TasksScope : ITasksScope
    {
        private readonly ILogger<TasksScope> logger;
        private readonly TaskHandler taskHandler;
        private readonly IEnumerable<ITask> tasks;

        private Guid guid { get; set; }

        public TasksScope(
            ILogger<TasksScope> logger,
            TaskHandler taskTracking,
            IEnumerable<ITask> tasks)
        {
            this.logger = logger;
            this.taskHandler = taskTracking;
            this.tasks = tasks;
            this.guid = Guid.NewGuid();
        }

        private List<Task> activeTasks = new List<Task>();


        /// <summary>
        /// Handle the task set in this service scope
        /// </summary>
        public async Task Handle(IServiceScope scope, CancellationToken token = default)
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
