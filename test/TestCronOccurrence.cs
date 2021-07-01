using System;
using NUnit.Framework;

namespace Oak.TaskScheduler.Test
{
    public class TestCronOccurrence
    {


        [SetUp]
        public void Setup()
        {
 
        }

        [Test]
        public void Test_Every_Minutes()
        {
            var occurance = new CronOccurrence("*/1 * * * *");
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 1, 0), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 1, 0), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 30));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t3);


            occurance = new CronOccurrence("*/3 * * * *");
            
            var t4 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 3, 0), t4);

            var t5 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 3, 0), t5);

            var t6 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 30));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t6);
        }
    }
}