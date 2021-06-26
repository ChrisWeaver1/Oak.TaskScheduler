using System;

namespace Scheduler.Cron
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