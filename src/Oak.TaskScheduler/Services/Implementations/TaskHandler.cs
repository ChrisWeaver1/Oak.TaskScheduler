using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Oak.TaskScheduler.Models;

namespace Oak.TaskScheduler.Services
{
    public class TaskHandler : ITaskHandler
    {
        private readonly ILogger<TaskHandler> logger;
        private ConcurrentDictionary<string, TaskTracker> tasks { get; set; }

        public TaskHandler(ILogger<TaskHandler> logger)
        {
            this.tasks = new ConcurrentDictionary<string, TaskTracker>();
            this.logger = logger;
        }

        public virtual async Task ExecuteTask(IScheduledTask task, CancellationToken token = default)
        {
            var tracker = this.retrieveTracker(task);

            if (!this.shouldTaskStart(task, ref tracker))
                return;

            tracker.TaskStarted(DateTime.UtcNow);
            tracker.NextRun = this.getNextRun(task, tracker.LastStarted ?? DateTime.UtcNow);

            this.logger.LogInformation($"Task Started: {task.GetName()} [Predicted start: {tracker.NextRun.ToString()}]");

            try
            {
                await task.Run(token);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                this.taskError(task, ref tracker);
                this.updateTasks(task, tracker);

                return;
            }

            this.completeTask(task, ref tracker);

            this.updateTasks(task, tracker);

            return;
        }

        private void completeTask(IScheduledTask task, ref TaskTracker tracker)
        {
            tracker.TaskCompleted(DateTime.UtcNow);
            this.logger.LogInformation($"Task Finished: {task.GetName()} [Completed: {tracker.Completed}, Errors: {tracker.Errors}, Average: {tracker.AverageRunTime.ToString()}, Next: {tracker.NextRun.ToString()}]");

            return;
        }

        private void taskError(IScheduledTask task, ref TaskTracker tracker)
        {
            tracker.TaskErrored(DateTime.UtcNow);
            this.logger.LogInformation($"Task Errored: {task.GetName()} [Completed: {tracker.Completed}, Errors: {tracker.Errors}, Average: {tracker.AverageRunTime.ToString()}, Next: {tracker.NextRun.ToString()}]");

            return;
        }

        private bool shouldTaskStart(IScheduledTask task, ref TaskTracker tracker)
        {
            if (tracker.LastStarted == null && task.RunOnStartUp)
                return true;

            if (DateTime.UtcNow <= tracker.NextRun)
                return false;

            if (tracker.IsRunning)
            {
                tracker.NextRun = this.getNextRun(task, DateTime.UtcNow);
                this.logger.LogWarning($"Task not finished, skipping run: {task.GetName()} [LastStart: {tracker.LastStarted.Value.ToString()}, NextRun: {tracker.NextRun.ToString()}]");
                return false;
            }

            return true;
        }

        private TaskTracker retrieveTracker(IScheduledTask task)
        {
            var tracker = this.tasks.GetValueOrDefault(task.GetName());

            if (tracker == null)
            {
                tracker = new TaskTracker
                {
                    NextRun = this.getNextRun(task, DateTime.UtcNow),
                };

                this.updateTasks(task, tracker);
            }

            return tracker;
        }

        private DateTime getNextRun(IScheduledTask task, DateTime lastRun)
        {
            return task.Occurrence.Next(lastRun);
        }

        private bool updateTasks(IScheduledTask task, TaskTracker tracker)
        {
            this.tasks.AddOrUpdate(task.GetName(), tracker, (name, _tracker) => 
            { 
                if (!this.tasks.TryUpdate(task.GetName(), tracker, _tracker))
                    this.logger.LogInformation("Failed to update dict"); 
                return tracker;
            });

            return true;
        }
    }
}
