using System;

namespace Oak.TaskScheduler
{
    public class SchedulerOptions
    {
        public int IterationScopeLimit { get; set; } = 100;
        public int IterationDelayMs { get; set; } = 5000;
        public bool IncludeRuntimeInDelay { get; set; } = true;
    }
}