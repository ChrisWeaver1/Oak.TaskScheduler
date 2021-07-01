using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler.Services
{
    /// <summary>
    /// Executes individual tasks
    /// </summary>
    public interface ITaskHandler
    {
        Task ExecuteTask(IScheduledTask task, CancellationToken token = default);
    }
}
