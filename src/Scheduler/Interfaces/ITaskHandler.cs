using System.Threading;
using System.Threading.Tasks;

namespace Scheduler
{
    public interface ITaskHandler
    {
        Task ExecuteTask(ITask task, CancellationToken token = default);
    }
}
