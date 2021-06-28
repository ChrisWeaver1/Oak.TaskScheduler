using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Oak.TaskScheduler.Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    options.ServiceName = "Task Scheduler Example";
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IScheduledTask, Task1>();
                    services.AddTransient<IScheduledTask, Task2>();
                    services.AddTransient<IScheduledTask, Task3>();
                    services.Configure<DbOptions>(hostContext.Configuration.GetSection("Database"));
                    
                    services.AddDbContext<DatabaseContext>();


                    services.AttachScheduler();
                });
    }
}
