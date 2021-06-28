using System;
using Weav.TaskScheduler.Cron;

namespace Weav.TaskScheduler
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