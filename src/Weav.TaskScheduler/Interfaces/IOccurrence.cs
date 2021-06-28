using System;
using System.Threading;
using System.Threading.Tasks;

namespace Weav.TaskScheduler
{
    public interface IOccurrence
    {
        DateTime Next(DateTime from);
    }
}
