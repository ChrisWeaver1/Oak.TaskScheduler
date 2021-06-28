using System.Threading;
using System.Threading.Tasks;

namespace Weav.TaskScheduler
{
    public interface IScheduler
    {
        Task Start(CancellationToken stoppingToken);
    }
}
