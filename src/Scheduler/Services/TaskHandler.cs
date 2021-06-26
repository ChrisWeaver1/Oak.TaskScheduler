using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Scheduler.Models;

namespace Scheduler
{
    /// <summary>
    /// Tracks & executes individual tasks
    /// </summary>
    public class TaskHandler : ITaskHandler
    {
        private readonly ILogger<TaskHandler> logger;
        private Dictionary<string, TaskTracker> tasks { get; set; }

        public TaskHandler(ILogger<TaskHandler> logger)
        {
            this.tasks = new Dictionary<string, TaskTracker>();
            this.logger = logger;
        }

        /// <summary>
        /// Check, track & execute tasks if its time to
        /// </summary>
        public async Task ExecuteTask(ITask task, CancellationToken token = default)
        {
            var tracker = this.retrieveTracker(task);

            if (!this.shouldTaskStart(task, ref tracker))
                return;

            tracker.TaskStarted(DateTime.UtcNow);
            tracker.NextRun = this.getNextRun(task, tracker.LastStarted ?? DateTime.UtcNow);

            this.logger.LogInformation($"Task Started: {task.Name} [Predicted start: {tracker.NextRun.ToString()}]");

            try
            {
                await task.Run(token);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                this.taskError(task, ref tracker);
                return;
            }

            this.completeTask(task, ref tracker);

            return;
        }

        private void completeTask(ITask task, ref TaskTracker tracker)
        {
            tracker.TaskCompleted(DateTime.UtcNow);
            this.logger.LogInformation($"Task Finished: {task.Name} [Completed: {tracker.Completed}, Errors: {tracker.Errors}, Average: {tracker.AverageRunTime.ToString()}, Next: {tracker.NextRun.ToString()}]");

            return;
        }

        private void taskError(ITask task, ref TaskTracker tracker)
        {
            tracker.TaskErrored(DateTime.UtcNow);
            this.logger.LogInformation($"Task Errored: {task.Name} [Completed: {tracker.Completed}, Errors: {tracker.Errors}, Average: {tracker.AverageRunTime.ToString()}, Next: {tracker.NextRun.ToString()}]");

            return;
        }

        private bool shouldTaskStart(ITask task, ref TaskTracker tracker)
        {
            if (tracker.LastStarted == null && task.RunOnStartUp)
                return true;

            if (DateTime.UtcNow <= tracker.NextRun)
                return false;

            if (tracker.IsRunning)
            {
                tracker.NextRun = this.getNextRun(task, DateTime.UtcNow);
                this.logger.LogWarning($"Task not finished, skipping run: {task.Name} [LastStart: {tracker.LastStarted.Value.ToString()}, NextRun: {tracker.NextRun.ToString()}]");
                return false;
            }

            return true;
        }

        private TaskTracker retrieveTracker(ITask task)
        {
            var tracker = this.tasks.GetValueOrDefault(task.Name);

            if (tracker == null)
            {
                tracker = new TaskTracker
                {
                    NextRun = this.getNextRun(task, DateTime.UtcNow),
                };

                this.tasks.Add(task.Name, tracker);
            }

            return tracker;
        }

        private DateTime getNextRun(ITask task, DateTime lastRun)
        {
            return task.Occurrence.Next(lastRun);
        }
    }
}
