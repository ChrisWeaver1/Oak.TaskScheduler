using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler
{
    public interface ITask
    {
        string Name { get; }
        IOccurrence Occurrence{ get; }
        bool RunOnStartUp { get; }
        Task Run(CancellationToken token = default);
    }
}
