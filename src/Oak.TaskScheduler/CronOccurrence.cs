using System;
using Oak.TaskScheduler.Cron;

namespace Oak.TaskScheduler
{
    public class CronOccurrence : IOccurrence
    {
        private CrontabSchedule schedule;

        public CronOccurrence(string expression)
        {
            this.schedule = new CrontabSchedule(expression);
        }

        public DateTime Next(DateTime from)
        {
            return this.schedule.GetNextOccurrence(from);
        }
    }
}
