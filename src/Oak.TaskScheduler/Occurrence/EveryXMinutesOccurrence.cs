using System;

namespace Oak.TaskScheduler
{
    public class EveryXMinutesOccurrence : IOccurrence
    {

        private readonly int amount;

        /// <summary>
        /// Run once every X minutes
        /// </summary>
        /// <param name="minutes">Number of minutes between occurrences</param>
        public EveryXMinutesOccurrence(int minutes)
        {
            this.amount = minutes;
        }

        public DateTime Next(DateTime from)
        {
            return this.minutes(from);
        }
        
        private DateTime minutes(DateTime from)
        {
            var next = new DateTime(from.Year, from.Month, from.Day, from.Hour, 0, 0);
            var minutes = (from.Minute - (from.Minute % this.amount)) + this.amount;
            next = next.AddMinutes(minutes);

            if ((next - from) > new TimeSpan(0, this.amount, 0))
                next = next.AddMinutes(-this.amount);   
            
            return next;
        }
    }
}

