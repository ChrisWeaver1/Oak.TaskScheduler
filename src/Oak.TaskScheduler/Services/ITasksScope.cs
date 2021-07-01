using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Oak.TaskScheduler.Services
{
    public interface ITasksScope
    {
        /// <summary>
        /// Handles execution of a new task set in a new service scope.
        /// This will run through each of the tasks & pass them through to the TaskHandler
        /// </summary>
        Task Handle(IServiceScope scope, CancellationToken token = default);
    }
}
