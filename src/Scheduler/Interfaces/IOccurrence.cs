using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler
{
    public interface IOccurrence
    {
        DateTime Next(DateTime from);
    }
}
