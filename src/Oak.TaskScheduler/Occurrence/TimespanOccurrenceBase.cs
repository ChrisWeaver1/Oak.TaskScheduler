using System;

namespace Oak.TaskScheduler
{
    public abstract class TimespanOccurrenceBase
    {
        protected TimespanOccurrenceBase()
        {
        }

        protected abstract TimeSpan timespan { get; }
        protected virtual TimeSpan offset { get; } = new TimeSpan(0);

        public virtual DateTime Next(DateTime from)
        {
            return this.calculate(from);
        }

        protected virtual DateTime calculate(DateTime from)
        {
            var dateTicks = from.Ticks;
            var timespanTicks = this.timespan.Ticks;

            var last = dateTicks - (dateTicks % (timespanTicks));
            var next = last + (timespanTicks);

            var date = new DateTime(next);

            if (this.offset != null)
                date = date + offset;

            return date;
        }
    }
}

