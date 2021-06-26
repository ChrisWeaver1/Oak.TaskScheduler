using System.Threading;
using System.Threading.Tasks;

namespace Scheduler
{
    public interface IScheduler
    {
        Task Start(CancellationToken stoppingToken);
    }
}
