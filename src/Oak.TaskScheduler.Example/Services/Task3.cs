using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Oak.TaskScheduler.Example
{
    public class Task3 : IScheduledTask
    {
        private readonly ILogger<Task3> logger;
        private readonly DatabaseContext context;

        private Guid guid { get; set; }

        public Task3(ILogger<Task3> logger, DatabaseContext context)
        {
            this.logger = logger;
            this.context = context;
            this.guid = Guid.NewGuid();
        }

        public string Name => "Task3";
        public IOccurrence Occurrence => new CronOccurrence("*/1 * * * *");

        public bool RunOnStartUp => true;

        public async Task Run(CancellationToken token = default)
        {
            this.logger.LogInformation($"{this.Name} triggered [{this.guid.ToString()}]");

            await context.Log.AddAsync(new Log
            {
                Identifier = this.Name,
                Date = DateTime.Now
            });

            await context.SaveChangesAsync();

            return;
        }
    }
}
