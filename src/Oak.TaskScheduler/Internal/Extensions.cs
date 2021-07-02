namespace Oak.TaskScheduler.Models
{
    internal static class Extensions
    {
        public static string GetName(this IScheduledTask task)
        {
            return task.GetType().ToString();
        }
    }
}