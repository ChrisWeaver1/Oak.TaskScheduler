using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Oak.TaskScheduler.Example
{
    public class Task2 : IScheduledTask
    {
        private readonly ILogger<Task2> logger;
        private Guid guid { get; set; }

        public Task2(ILogger<Task2> logger)
        {
            this.logger = logger;
            this.guid = Guid.NewGuid();
        }

        public string Name => "Task2";

        public IOccurrence Occurrence => new EveryXDaysOccurrence(1, 16);

        public bool RunOnStartUp => true;

        public Guid Id { get; set; }

        public async Task Run(CancellationToken token = default)
        {
            this.logger.LogInformation($"{this.Name} triggered [{this.guid.ToString()}]");

            await Task.Delay(25000, token);

            return;
        }
    }
}
