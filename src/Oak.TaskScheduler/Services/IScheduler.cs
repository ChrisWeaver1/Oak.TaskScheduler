using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler.Services
{    
    /// <summary>
    /// Scheduler background service. Handles asyncronous & concurrent execution of tasks.
    /// </summary>
    public interface IScheduler
    {
        /// <summary>
        /// Start processing tasks
        /// </summary>
        Task Start(CancellationToken stoppingToken);

        /// <summary>
        /// Stop scheduler after finishing the current iteration.
        /// Force cancellation should be handled through the CancellationToken provided to the start method. 
        /// </summary>
        void Stop();

        /// <summary>
        /// Action called on each iteration of loop
        /// </summary>
        Action OnIteration  { get; set; }
        /// <summary>
        /// Action to be called when the scope limit is reached
        /// </summary>
        Action OnIterationScopesLimit { get; set; }


    }
}
