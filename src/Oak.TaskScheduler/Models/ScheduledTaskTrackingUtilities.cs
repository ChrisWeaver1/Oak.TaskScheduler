using System;

namespace Oak.TaskScheduler.Models
{
    public class ScheduledTaskTrackingUtilities
    {
        public ScheduledTaskTrackingUtilities(ScheduledTaskTracking model)
        {
            this.ScheduledTaskTracking = model;
        }

        public string Name
        {
            get { return this.ScheduledTaskTracking.Name; }
            set { this.ScheduledTaskTracking.Name = value; }
        }

        public DateTime? LastStarted 
        { 
            get { return this.ScheduledTaskTracking.LastCompleted; }
            set { this.ScheduledTaskTracking.LastCompleted = value; }
        }

        public DateTime? LastCompleted 
        { 
            get { return this.ScheduledTaskTracking.LastCompleted; } 
            set { this.ScheduledTaskTracking.LastCompleted = value; }   
        }

        public DateTime NextRun 
        { 
            get { return this.ScheduledTaskTracking.NextRun; }
            set { this.ScheduledTaskTracking.NextRun = value; }
        }

        public int Completed 
        { 
            get { return this.ScheduledTaskTracking.Completed; } 
            set { this.ScheduledTaskTracking.Completed = value; } 
        }

        public int Errors 
        { 
            get { return this.ScheduledTaskTracking.Errors; }
            set { this.ScheduledTaskTracking.Errors = value; }
        }

        public TimeSpan? AverageRunTime 
        { 
            get { return this.ScheduledTaskTracking.AverageRunTime; } 
            set { this.ScheduledTaskTracking.AverageRunTime = value; } 
        }

        public ScheduledTaskTracking ScheduledTaskTracking { get; set; }

        public bool IsRunning { get; set; } = false;
        public int Runs => this.Completed + this.Errors;

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