using System.Linq;
using System.Threading.Tasks;
using Oak.TaskScheduler.Models;

namespace Oak.TaskScheduler.Data
{
    public class ScheduledTaskTrackingSessionRepository : IScheduledTaskTrackingRepository
    {
        private readonly TaskTrackingData data;

        public ScheduledTaskTrackingSessionRepository(TaskTrackingData data)
        {
            this.data = data;
        }

        public Task<ScheduledTaskTrackingUtilities> Retrieve(IScheduledTask task)
        {
            var t = this.data.ScheduledTaskTracking.FirstOrDefault(s => s.Name == task.Name);
            if (t == null)
            {
                t = new ScheduledTaskTracking { Name = task.Name };
                this.data.ScheduledTaskTracking.Add(t);
            }

            return Task.FromResult(new ScheduledTaskTrackingUtilities(t));
        }

        public Task Update(ScheduledTaskTrackingUtilities model)
        {
            var task = this.data.ScheduledTaskTracking.FirstOrDefault(s => s.Name == model.Name);

            task.LastStarted = model.LastStarted;
            task.LastCompleted = model.LastCompleted;
            task.NextRun = model.NextRun;
            task.Completed = model.Completed;
            task.Errors = model.Errors;
            task.AverageRunTime = model.AverageRunTime;

            return Task.CompletedTask;
        }


    }
}
