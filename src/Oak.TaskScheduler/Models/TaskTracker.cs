using System;

namespace Oak.TaskScheduler.Models
{
    public class TaskTracker
    {
        public DateTime? LastStarted { get; set; }
        public DateTime? LastCompleted { get; set; }
        public DateTime NextRun { get; set; }
        public int Completed { get; set; } = 0;
        public int Errors { get; set; } = 0;
        public bool IsRunning { get; set; } = false;


        public int Runs => this.Completed + this.Errors;
        public TimeSpan? AverageRunTime { get; set; } = null;

        public void TaskStarted(DateTime time)
        {
            this.IsRunning = true;
            this.LastStarted = time;
        }

        public void TaskCompleted(DateTime time)
        {
            this.IsRunning = false;
            this.LastCompleted = time;
            this.Completed += 1;

            this.calculateAverage();
        }

        public void TaskErrored(DateTime time)
        {
            this.IsRunning = false;
            this.LastCompleted = time;
            this.Errors += 1;

            this.calculateAverage();
        }

        private void calculateAverage()
        {
            if (this.AverageRunTime == null)
            {
                this.AverageRunTime = this.LastCompleted.Value - this.LastStarted.Value;
                return;
            }

            this.AverageRunTime = (this.AverageRunTime + (this.LastCompleted.Value - this.LastStarted.Value)) / 2;
        }
    }
}