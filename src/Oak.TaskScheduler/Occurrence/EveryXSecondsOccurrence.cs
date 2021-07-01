using System;

namespace Oak.TaskScheduler
{
    public class EveryXSecondsOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        /// <summary>
        /// Set a number of seconds between occurrences
        /// </summary>
        /// <param name="seconds">Number of seconds between occurrences</param>
        public EveryXSecondsOccurrence(int seconds) 
        {
            base.setValues(new TimeSpan(0, 0, seconds));
        }
    }
}

