using System;

namespace Oak.TaskScheduler
{
    public class EveryXHoursOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        /// <summary>
        /// Set a number of hours between occurrences
        /// </summary>
        /// <param name="hours">Number of hours between occurrences</param>
        /// <param name="minutesOffset">Offset the minutes by this amount. Example: 1 hour with a 30 minute offset would return values 30 minutes after each hour, rather than on the hour.</param>
        /// <param name="secondsOffset">Offset the seconds by this amount. Example: 1 minute with a 30 second offset would return values 30 seconds after the minute, rather than on the minute.</param>
        public EveryXHoursOccurrence(int hours, int minutesOffset = 0, int secondsOffset = 0) 
        {
            this.setValues(new TimeSpan(hours, 0, 0), new TimeSpan(0, minutesOffset, secondsOffset));
        }
    }
}

