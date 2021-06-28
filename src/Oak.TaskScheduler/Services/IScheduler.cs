using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler.Services
{
    public interface IScheduler
    {
        Task Start(CancellationToken stoppingToken);
    }
}
