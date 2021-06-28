using System;

namespace Oak.TaskScheduler
{
    public class EveryXHoursOccurrence : IOccurrence
    {

        private readonly int amount;
        private readonly int minuteOffset;

        /// <summary>
        /// Run task every X hours
        /// </summary>
        /// <param name="amount">Number of time units</param>
        /// <param name="minuteOffset">Offset minutes by this amount. To run at 25 minutes past the hour, you would pass in 25</param>
        public EveryXHoursOccurrence(int hours, int minuteOffset = 0)
        {
            this.minuteOffset = minuteOffset;
            this.amount = hours;
        }

        public DateTime Next(DateTime from)
        {
            return this.hours(from);
        }

        private DateTime hours(DateTime from)
        {
            var next = new DateTime(from.Year, from.Month, from.Day, 0, this.minuteOffset, 0);
            var hours = (from.Hour - (from.Hour % this.amount)) + this.amount;
            return next.AddHours(hours);
        }
    }
}

