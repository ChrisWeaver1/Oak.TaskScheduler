using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Oak.TaskScheduler.Services
{
    public interface ITasksScope
    {
        Task Handle(IServiceScope scope, CancellationToken token = default);
    }
}
