using System;

namespace Oak.TaskScheduler
{
    public abstract class TimespanOccurrenceBase
    {
        protected TimespanOccurrenceBase()
        {
        }

        protected virtual TimeSpan timespan { get; private set; } = default;
        protected virtual TimeSpan offset { get; private set; } = default;

        protected virtual void setValues(TimeSpan timespan, TimeSpan offset = default)
        {
            if (offset >= timespan)
                throw new Exception("Offset cannot exceed (or equal) the timespan between scheduled occurrences.");

            this.timespan = timespan;
            this.offset = offset;
        }

        public virtual DateTime Next(DateTime from)
        {
            return this.calculate(from);
        }

        protected virtual DateTime calculate(DateTime from)
        {
            // Convert to ticks
            var dateTicks = from.Ticks;
            var timespanTicks = this.timespan.Ticks;

            // Find the last matching value by taking the remainder of from / timespan
            // and substracting it from the from date
            var last = dateTicks - (dateTicks % timespanTicks);
            
            // Add the occurrence timespan to the last value to get the next occurrence value
            var next = last + timespanTicks;

            var date = new DateTime(next);

            // Add the offset to the next occurrence
            if (this.offset != null && this.offset != default)
                date = date + offset;

            // If the offset has caused the timespan between runs to be greater
            // than the scheduled occurrence, offset the value by the timespan
            if ((date - from) > this.timespan)
                date = date.Add(-this.timespan);

            return date;
        }
    }
}

