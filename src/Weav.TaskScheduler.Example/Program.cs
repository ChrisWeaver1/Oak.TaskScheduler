using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Weav.TaskScheduler.Example
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
                    services.AddTransient<ITask, Task1>();
                    services.AddTransient<ITask, Task2>();
                    services.AddTransient<ITask, Task3>();
                    services.Configure<DbOptions>(hostContext.Configuration.GetSection("Database"));
                    
                    services.AddDbContext<DatabaseContext>();


                    services.AttachScheduler();
                });
    }
}
