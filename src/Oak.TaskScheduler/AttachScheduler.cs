using System;
using Microsoft.Extensions.DependencyInjection;
using Oak.TaskScheduler.Data;
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
            if (options == null)
            {
                options = new Action<SchedulerOptions>((c) => {});
            }

            serviceCollection.AddHostedService<Scheduler>();
            serviceCollection.AddSingleton<TaskTrackingData, TaskTrackingData>();
            serviceCollection.AddTransient<ITaskHandler, TaskHandler>();
            serviceCollection.AddTransient<IScheduledTaskTrackingRepository, ScheduledTaskTrackingSessionRepository>();
            serviceCollection.AddScoped<ITasksScope, TasksScope>();
            serviceCollection.Configure<SchedulerOptions>(options);
        }
    }
}
