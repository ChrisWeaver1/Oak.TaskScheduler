using System;
using System.Threading;
using System.Threading.Tasks;

namespace Oak.TaskScheduler
{
    public interface IOccurrence
    {
        DateTime Next(DateTime from);
    }
}
