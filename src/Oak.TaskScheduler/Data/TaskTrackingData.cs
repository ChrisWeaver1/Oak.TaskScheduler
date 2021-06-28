using System.Collections.Generic;
using Oak.TaskScheduler.Models;

namespace Oak.TaskScheduler.Data
{
    public class TaskTrackingData
    {
        public List<ScheduledTaskTracking> ScheduledTaskTracking { get; set; } = new List<ScheduledTaskTracking> { };
    }
}
