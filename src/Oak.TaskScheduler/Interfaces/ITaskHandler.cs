using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler
{
    public interface ITaskHandler
    {
        Task ExecuteTask(ITask task, CancellationToken token = default);
    }
}
