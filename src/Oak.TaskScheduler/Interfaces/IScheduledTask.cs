using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler
{
    public interface IScheduledTask
    {
        IOccurrence Occurrence { get; }
        bool RunOnStartUp { get; }
        Task Run(CancellationToken token = default);
    }
}
