using System;
using Microsoft.Extensions.DependencyInjection;
using Oak.TaskScheduler.Services;

namespace Oak.TaskScheduler
{
    public static class ConfigureScheduler
    {
        /// <summary>
        /// Add Scheduler and other required services.
        /// </summary>
        public static void AttachHostedScheduler(this IServiceCollection serviceCollection, Action<SchedulerOptions> options = null)
        {
            // use default options
            if (options == null)
                options = new Action<SchedulerOptions>((c) => {});
            
            serviceCollection.AddHostedService<Scheduler>();
            serviceCollection.AddSingleton<ITaskHandler, TaskHandler>();
            serviceCollection.AddScoped<ITasksScope, TasksScope>();
            serviceCollection.Configure<SchedulerOptions>(options);
        }
    }
}
