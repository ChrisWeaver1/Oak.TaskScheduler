using NUnit.Framework;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Oak.TaskScheduler.Test
{
    public class MockTasks
    {
        public Mock<ITask> Default() 
        {
            var mock = new Mock<ITask>();

            mock.Setup(m => m.Name).Returns("Default");
            mock.Setup(m => m.RunOnStartUp).Returns(false);
            mock.Setup(m => m.Run(It.IsNotNull<CancellationToken>())).Returns(Task.CompletedTask);
            mock.Setup(m => m.Occurrence.Next(It.IsAny<DateTime>())).Returns((DateTime d) => { return d.AddMinutes(1); });
            return mock;
        }

        public Mock<ITask> Daily() 
        {
            var mock = new Mock<ITask>();

            mock.Setup(m => m.Name).Returns("Daily");
            mock.Setup(m => m.RunOnStartUp).Returns(false);
            mock.Setup(m => m.Run(It.IsNotNull<CancellationToken>())).Returns(async (CancellationToken c) => { await Task.Delay(2000); return; });
            mock.Setup(m => m.Occurrence.Next(It.IsAny<DateTime>())).Returns((DateTime d) => { return d.Date.AddDays(1).AddHours(4); });
            return mock;
        }

        public Mock<ITask> Startup() 
        {
            var mock = new Mock<ITask>();

            mock.Setup(m => m.Name).Returns("Startup");
            mock.Setup(m => m.RunOnStartUp).Returns(true);
            mock.Setup(m => m.Run(It.IsNotNull<CancellationToken>())).Returns(Task.CompletedTask);
            mock.Setup(m => m.Occurrence.Next(It.IsAny<DateTime>())).Returns((DateTime d) => { return d.AddMinutes(1); });
            return mock;
        }
    }
}