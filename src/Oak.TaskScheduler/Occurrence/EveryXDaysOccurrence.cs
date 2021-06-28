using System;

namespace Oak.TaskScheduler
{
    public class EveryXDaysOccurrence : IOccurrence
    {

        private readonly int amount;
        private readonly int hourOffset;
        private readonly int minuteOffset;

        /// <summary>
        /// Run task every X days
        /// </summary>
        /// <param name="amount">Number of time units</param>
        /// <param name="hourOffset">Offset hours by this amount. To run at 8am, pass through 8</param>
        /// <param name="minuteOffset">Offset minutes by this amount. To run at 25 minutes past the hour, you would pass in 25</param>
        public EveryXDaysOccurrence(int amount, int hourOffset = 0, int minuteOffset = 0)
        {
            this.hourOffset = hourOffset;
            this.minuteOffset = minuteOffset;
            this.amount = amount;
        }

        public DateTime Next(DateTime from)
        {
            return this.days(from);
        }

        private DateTime days(DateTime from)
        {
            var next = new DateTime(from.Year, from.Month, 0, this.hourOffset, this.minuteOffset, 0);
            var days = (from.Day - (from.Day % this.amount)) + this.amount;
            next = next.AddDays(days);

            if ((next - from) > new TimeSpan(this.amount, 0, 0, 0 ,0))
                next = next.AddDays(-this.amount);   
        }
    }
}

