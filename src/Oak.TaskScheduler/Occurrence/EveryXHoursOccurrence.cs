using System;

namespace Oak.TaskScheduler
{
    public class EveryXHoursOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        private readonly int hours;
        private readonly TimeSpan offset1;

        /// <summary>
        /// Run once every X hours
        /// </summary>
        /// <param name="hours">Number of hours between occurrences</param>
        public EveryXHoursOccurrence(int hours, int minutesOffset = 0, int secondsOffset = 0) 
        {
            this.hours = hours;
            this.offset1 = new TimeSpan(0 , minutesOffset, secondsOffset);
        }

        protected override TimeSpan timespan => new TimeSpan(this.hours, 0, 0);
        protected override TimeSpan offset => this.offset1;
    }
}

