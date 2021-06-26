using System;

namespace Scheduler
{
    public class SchedulerOptions
    {
        public int IterationScopeLimit { get; set; } = 100;
        public int IterationDelayMs { get; set; } = 5000;
    }
}