using System;
using Microsoft.Extensions.DependencyInjection;

namespace Scheduler
{
    public static class ConfigureScheduler
    {
        /// <summary>
        /// Add Scheduler and other required services.
        /// </summary>
        public static void AttachScheduler(this IServiceCollection serviceCollection, Action<SchedulerOptions> options = null)
        {
            if (options == null)
            {
                options = new Action<SchedulerOptions>((c) => {});
            }

            serviceCollection.AddHostedService<Scheduler>();
            serviceCollection.AddSingleton<ITaskHandler, TaskHandler>();
            serviceCollection.AddScoped<ITasksScope, TasksScope>();
            serviceCollection.Configure<SchedulerOptions>(options);
        }
    }
}
