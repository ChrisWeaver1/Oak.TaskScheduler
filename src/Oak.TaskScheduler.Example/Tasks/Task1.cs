using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Oak.TaskScheduler.Example
{
    public class Task1 : IScheduledTask
    {
        private readonly ILogger<Task1> logger;
        private Guid guid { get; set; }

        public Task1(ILogger<Task1> logger)
        {
            this.logger = logger;
            this.guid = Guid.NewGuid();
        }

        public string Name => "Task1";

        public IOccurrence Occurrence => new CronOccurrence("*/1 * * * *");

        public bool RunOnStartUp => false;

        public async Task Run(CancellationToken token = default)
        {
            this.logger.LogInformation($"{this.Name} triggered [{this.guid.ToString()}]");

            await Task.Delay(2000, token);

            //throw new Exception("!");
            
            return;
        }
    }
}
