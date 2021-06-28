using System;

namespace Oak.TaskScheduler.Cron
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