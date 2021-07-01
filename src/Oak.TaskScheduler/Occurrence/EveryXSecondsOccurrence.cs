using System;

namespace Oak.TaskScheduler
{
    public class EveryXSecondsOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        private readonly int seconds;

        /// <summary>
        /// Run once every X seconds
        /// </summary>
        /// <param name="seconds">Number of seconds between occurrences</param>
        public EveryXSecondsOccurrence(int seconds) 
        {
            this.seconds = seconds;
        }

        protected override TimeSpan timespan => new TimeSpan(0, 0, this.seconds);
    }
}

