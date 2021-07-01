using System;

namespace Oak.TaskScheduler
{
    public class TimespanOccurrence : TimespanOccurrenceBase, IOccurrence
    {
        private readonly TimeSpan timespan1;
        private readonly TimeSpan offset1;

        /// <summary>
        /// Run once every X seconds
        /// </summary>
        /// <param name="seconds">Number of seconds between occurrences</param>
        public TimespanOccurrence(TimeSpan timespan1, TimeSpan offset1 = default) 
        {
            this.timespan1 = timespan1;
            this.offset1 = offset1;
        }

        protected override TimeSpan timespan => this.timespan1;
        protected override TimeSpan offset => this.offset1;

    }
}

