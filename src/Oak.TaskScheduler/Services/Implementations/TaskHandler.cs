using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Oak.TaskScheduler.Data;
using Oak.TaskScheduler.Models;

namespace Oak.TaskScheduler.Services
{
    /// <summary>
    /// Tracks & executes individual tasks
    /// </summary>
    public class TaskHandler : ITaskHandler
    {
        private readonly ILogger<TaskHandler> logger;
        private readonly IScheduledTaskTrackingRepository repository;

        public TaskHandler(ILogger<TaskHandler> logger, IScheduledTaskTrackingRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        /// <summary>
        /// Check, track & execute tasks if its time to
        /// </summary>
        public async Task ExecuteTask(IScheduledTask task, CancellationToken token = default)
        {
            var tracker = await this.retrieveTracker(task);

            if (!this.shouldTaskStart(task, tracker))
                return;

            await this.taskStarted(task, tracker);

            try
            {
                await task.Run(token);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.ToString());
                await this.taskError(task, tracker);
                return;
            }

            await this.completeTask(task, tracker);

            return;
        }

        private Task taskStarted(IScheduledTask task, ScheduledTaskTrackingUtilities tracker)
        {
            tracker.TaskStarted(DateTime.UtcNow);
            tracker.NextRun = this.getNextRun(task, tracker.LastStarted ?? DateTime.UtcNow);

            this.logger.LogInformation($"Task Started: {task.Name} [Predicted start: {tracker.NextRun.ToString()}]");

            return this.repository.Update(tracker);
        }

        private Task completeTask(IScheduledTask task, ScheduledTaskTrackingUtilities tracker)
        {
            tracker.TaskCompleted(DateTime.UtcNow);
            this.logger.LogInformation($"Task Finished: {task.Name} [Completed: {tracker.Completed}, Errors: {tracker.Errors}, Average: {tracker.AverageRunTime.ToString()}, Next: {tracker.NextRun.ToString()}]");

            return this.repository.Update(tracker);
        }

        private Task taskError(IScheduledTask task, ScheduledTaskTrackingUtilities tracker)
        {
            tracker.TaskErrored(DateTime.UtcNow);
            this.logger.LogInformation($"Task Errored: {task.Name} [Completed: {tracker.Completed}, Errors: {tracker.Errors}, Average: {tracker.AverageRunTime.ToString()}, Next: {tracker.NextRun.ToString()}]");

            return this.repository.Update(tracker);
        }

        private bool shouldTaskStart(IScheduledTask task, ScheduledTaskTrackingUtilities tracker)
        {
            if (tracker.LastStarted == null && task.RunOnStartUp)
                return true;

            if (DateTime.UtcNow <= tracker.NextRun)
                return false;

            // may reimplement this isRunning check in the future - just no great way of doing it right now
            // if (tracker.IsRunning)
            // {
            //     tracker.NextRun = this.getNextRun(task, DateTime.UtcNow);
            //     this.logger.LogWarning($"Task not finished, skipping run: {task.Name} [LastStart: {tracker.LastStarted.Value.ToString()}, NextRun: {tracker.NextRun.ToString()}]");
            //     return false;
            // }

            return true;
        }

        private Task<ScheduledTaskTrackingUtilities> retrieveTracker(IScheduledTask task)
        {
            return this.repository.Retrieve(task);
        }

        private DateTime getNextRun(IScheduledTask task, DateTime lastRun)
        {
            return task.Occurrence.Next(lastRun);
        }
    }
}
