using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Scheduler.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<ITask, Task1>();
                    services.AddTransient<ITask, Task2>();
                    services.AddTransient<ITask, Task3>();
                    services.AddDbContext<DatabaseContext>();

                    services.AttachScheduler();
                });
    }
}
