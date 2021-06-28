using System.Threading.Tasks;
using Oak.TaskScheduler.Models;

namespace Oak.TaskScheduler.Data
{
    public interface IScheduledTaskTrackingRepository
    {
        Task<ScheduledTaskTrackingUtilities> Retrieve(IScheduledTask task);
        Task Update(ScheduledTaskTrackingUtilities model);
    }
}
