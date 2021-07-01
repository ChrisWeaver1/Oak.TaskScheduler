using System;

namespace Oak.TaskScheduler
{
    /// <summary>
    /// See CronOccurrence, EveryXDaysOccurrence, EveryXHoursOccurrence and EveryXMinutesOccurrence for implemention examples.
    /// </summary>
    public interface IOccurrence
    {
        /// <summary>
        /// Calculates the next occurance of a task from the provided date
        /// </summary>
        DateTime Next(DateTime from);
    }
}
