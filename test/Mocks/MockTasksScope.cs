using Moq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Scheduler.Test
{
    public class MockTasksScope
    {
        public int disposals { get; set; } = 0;

        public int handleCalls { get; set; } = 0;


        public Mock<ITasksScope> Default()
        {
            var mock = new Mock<ITasksScope>();
            mock.Setup(m => m.Handle(It.IsAny<IServiceScope>(), It.IsAny<CancellationToken>())).Callback(() => { this.handleCalls += 1; });
            return mock;
        }

        public Mock<IServiceScope> Scope() 
        {
            var mock = new Mock<IServiceScope>();
            mock.Setup(m => m.Dispose()).Callback(() => { this.disposals += 1; });
            return mock;
        }
    }

    public class FakeTaskScope : ITasksScope
    {
        public async Task Handle(IServiceScope scope, CancellationToken token = default)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(1000);
            }

            scope.Dispose();
        }
    }
}