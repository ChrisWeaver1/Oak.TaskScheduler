using System;

namespace Oak.TaskScheduler.Models
{
    public class ScheduledTaskTracking
    {
        public string Name { get; set; }
        public DateTime? LastStarted { get; set; }
        public DateTime? LastCompleted { get; set; }
        public DateTime NextRun { get; set; }
        public int Completed { get; set; } = 0;
        public int Errors { get; set; } = 0;
        public TimeSpan? AverageRunTime { get; set; } = null;
    }
}