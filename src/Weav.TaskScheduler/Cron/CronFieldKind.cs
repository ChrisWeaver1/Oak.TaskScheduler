using System;

namespace Weav.TaskScheduler.Cron
{
    [Serializable]
    internal enum CrontabFieldKind
    {
        Minute,
        Hour,
        Day,
        Month,
        DayOfWeek
    }
}