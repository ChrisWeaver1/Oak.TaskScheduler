using System;

namespace Oak.TaskScheduler
{
    public class EveryXDaysOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        private readonly int days;
        private readonly TimeSpan offset1;

        /// <summary>
        /// Run once every X days
        /// </summary>
        /// <param name="days">Number of days between occurrences</param>
        public EveryXDaysOccurrence(int days, int hoursOffset = 0, int minutesOffset = 0, int secondsOffset = 0) 
        {
            this.days = days;
            this.offset1 = new TimeSpan(hoursOffset , minutesOffset, secondsOffset);
        }

        protected override TimeSpan timespan => new TimeSpan(this.days, 0, 0, 0);
        protected override TimeSpan offset => this.offset1;
    }
}

