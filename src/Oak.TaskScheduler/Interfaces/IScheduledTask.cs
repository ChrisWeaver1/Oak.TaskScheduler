using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler
{
    public interface IScheduledTask
    {
        string Name { get; }
        IOccurrence Occurrence{ get; }
        bool RunOnStartUp { get; }
        Task Run(CancellationToken token = default);
    }
}
