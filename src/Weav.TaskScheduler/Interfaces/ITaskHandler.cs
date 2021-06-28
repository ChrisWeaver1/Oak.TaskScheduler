using System.Threading;
using System.Threading.Tasks;

namespace Weav.TaskScheduler
{
    public interface ITaskHandler
    {
        Task ExecuteTask(ITask task, CancellationToken token = default);
    }
}
