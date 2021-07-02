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

        public IOccurrence Occurrence => new CronOccurrence("*/1 * * * *");
        public bool RunOnStartUp => true;

        public async Task Run(CancellationToken token = default)
        {
            this.logger.LogInformation($"{this.GetType().ToString()} triggered [{this.guid.ToString()}]");

            await context.Log.AddAsync(new Log
            {
                Identifier = this.GetType().ToString(),
                Date = DateTime.Now
            });

            await context.SaveChangesAsync();

            return;
        }
    }
}
