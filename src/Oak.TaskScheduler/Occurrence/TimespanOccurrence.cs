using System;

namespace Oak.TaskScheduler
{
    public class TimespanOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        /// <summary>
        /// Set a timespan between occurrences
        /// </summary>
        /// <param name="timespan">Timespan between occurrences</param>
        /// <param name="offset">Offset the time by this value. With a timespan of 1 hour & a offset of 30 minutes, this would return values at 30 minutes past every hour.</param>
        public TimespanOccurrence(TimeSpan timespan, TimeSpan offset = default) 
        {
            base.setValues(timespan, offset);
        }
    }
}

