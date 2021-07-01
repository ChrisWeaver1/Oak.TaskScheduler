using System;

namespace Oak.TaskScheduler
{
    public class EveryXDaysOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        /// <summary>
        /// Set a number of days between occurrences
        /// </summary>
        /// <param name="days">Number of days between occurrences</param>
        /// <param name="hoursOffset">Offset the hours by this amount. Example: 1 day with a 3 hour offset would return values at 3AM every day, rather than at midnight.</param>
        /// <param name="minutesOffset">Offset the minutes by this amount. Example: 1 hour with a 30 minute offset would return values 30 minutes after each hour, rather than on the hour.</param>
        /// <param name="secondsOffset">Offset the seconds by this amount. Example: 1 minute with a 30 second offset would return values 30 seconds after the minute, rather than on the minute.</param>
        public EveryXDaysOccurrence(int days, int hoursOffset = 0, int minutesOffset = 0, int secondsOffset = 0) 
        {
            this.setValues(new TimeSpan(days, 0, 0, 0), new TimeSpan(hoursOffset, minutesOffset, secondsOffset));
        }
    }
}

