using System;

namespace Oak.TaskScheduler
{
    public class EveryXMinutesOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        private readonly int minutes;
        private readonly TimeSpan offset1;

        /// <summary>
        /// Run once every X minutes
        /// </summary>
        /// <param name="minutes">Number of minutes between occurrences</param>
        public EveryXMinutesOccurrence(int minutes, int secondsOffset = 0) 
        {
            this.minutes = minutes;
            this.offset1 = new TimeSpan(0 , 0, secondsOffset);
        }

        protected override TimeSpan timespan => new TimeSpan(0, this.minutes, 0);
        protected override TimeSpan offset => this.offset1;
    }
}

