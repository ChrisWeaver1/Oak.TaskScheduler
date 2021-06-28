using Moq;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Oak.TaskScheduler.Test
{
    public class MockServiceProvider
    {
        private MockTasksScope tasksScope;

        public MockServiceProvider()
        {
            this.tasksScope = new MockTasksScope();
        }

        public IServiceProvider Provider()
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddScoped<ITasksScope, FakeTaskScope>();

            return collection.BuildServiceProvider();
        }

        public IServiceScope Scope()
        {
            return this.Provider().CreateScope();
        }
    }
}