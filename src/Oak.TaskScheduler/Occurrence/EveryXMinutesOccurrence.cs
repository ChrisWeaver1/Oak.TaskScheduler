using System;

namespace Oak.TaskScheduler
{
    public class EveryXMinutesOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        /// <summary>
        /// Set a number of minutes between occurrences
        /// </summary>
        /// <param name="minutes">Number of minutes between occurrences</param>
        /// <param name="secondsOffset">Offset the seconds by this amount. Example: 1 minute with a 30 second offset would return values 30 seconds after the minute, rather than on the minute.</param>
        public EveryXMinutesOccurrence(int minutes, int secondsOffset = 0) 
        {
            base.setValues(new TimeSpan(0, minutes, 0), new TimeSpan(0, 0, secondsOffset));
        }
    }
}

