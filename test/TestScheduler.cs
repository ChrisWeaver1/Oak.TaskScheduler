using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Oak.TaskScheduler.Test
{
    public class TestScheduler
    {
        private MockServiceProvider providers;
        private MockOptions options;

        [SetUp]
        public void Setup()
        {
            this.options = new MockOptions();
            this.providers = new MockServiceProvider();
 
        }

        [Test]
        public async Task Test_Cancellation()
        {
            this.Setup();

            var opts = this.options.Default(new SchedulerOptions 
            {
                IterationDelayMs = 1000,
                IterationScopeLimit = 100,
            });

            var scheduler = new Scheduler(providers.Provider(), new Mock<ILogger<Scheduler>>().Object, opts.Object);

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            int loops = 0;
            scheduler.OnIteration = () => { loops += 1; };

            var task = scheduler.Start(token);
            
            source.CancelAfter(1500);
            try
            {
                await task;
            }
            catch(Exception ex)
            {
                // will throw an exception if cancelled on a delay - might catch this in code instead
                Assert.AreEqual(ex.Message, "A task was canceled.");
            }

            Assert.AreEqual(loops, 2);
        }


        [Test]
        public async Task Test_Iterations()
        {
            this.Setup();

            var opts = this.options.Default(new SchedulerOptions 
            {
                IterationDelayMs = 200,
                IterationScopeLimit = 100,
            });

            var scheduler = new Scheduler(providers.Provider(), new Mock<ILogger<Scheduler>>().Object, opts.Object);

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            int loops = 0;
            scheduler.OnIteration = () => { loops += 1; };

            var task = scheduler.Start(token);

            await Task.Delay(100);

            Assert.AreEqual(1, loops);

            await Task.Delay(1000);

            Assert.AreEqual(6, loops);

            await Task.Delay(5000);

            Assert.AreEqual(31, loops);

            scheduler.Stop();

            await task;
        }
    
        [Test]
        public async Task Test_Iterations_Scopes_Limit()
        {
            this.Setup();

            var opts = this.options.Default(new SchedulerOptions 
            {
                IterationDelayMs = 10,
                IterationScopeLimit = 20,
            });

            var scheduler = new Scheduler(providers.Provider(), new Mock<ILogger<Scheduler>>().Object, opts.Object);

            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            int loops = 0;
            scheduler.OnIteration = () => { loops += 1; };

            var task = scheduler.Start(token);

            source.CancelAfter(10000);

            bool isHit = false;
            scheduler.OnIterationScopesLimit = () => 
            { 
                isHit = true; 
                scheduler.Stop();    
            };

            await task;

            Assert.IsTrue(isHit);
            // should hit the limit at 21 (20 limit + 1) & finsh that loop before stopping
            Assert.AreEqual(22, loops);
        }
    }
}