using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler
{
    public interface IScheduler
    {
        Task Start(CancellationToken stoppingToken);
    }
}
