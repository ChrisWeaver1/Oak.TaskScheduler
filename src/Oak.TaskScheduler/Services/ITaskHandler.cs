using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler.Services
{
    public interface ITaskHandler
    {
        Task ExecuteTask(IScheduledTask task, CancellationToken token = default);
    }
}
