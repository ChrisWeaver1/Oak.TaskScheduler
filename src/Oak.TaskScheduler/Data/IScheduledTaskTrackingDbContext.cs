using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oak.TaskScheduler.Models;

namespace Oak.TaskScheduler.Data
{
    public interface IScheduledTaskTrackingDbContext
    {
        DbSet<ScheduledTaskTracking> ScheduledTaskTracking { get; set; }
        Task<int> SaveChangesAsync();
    }
}
