using System;
using NUnit.Framework;

namespace Oak.TaskScheduler.Test
{
    public class TestEveryXSecondsOccurrence
    {
        [SetUp]
        public void Setup()
        {
 
        }

        [Test]
        public void Test_Every_1_Seconds()
        {
            var occurance = new EveryXSecondsOccurrence(1);
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 0, 1), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 0, 31), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 59));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t3);
        }

        [Test]
        public void Test_Every_3_Seconds()
        {
            var occurance = new EveryXSecondsOccurrence(3);
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 1));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 0, 3), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 0, 33), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 59));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t3);

            var t4 = occurance.Next(new DateTime(2021, 1, 1, 0, 0, 0));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 3), t4);
        }

        [Test]
        public void Test_Every_20_Seconds()
        {
            var occurance = new EveryXSecondsOccurrence(20);
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 0, 20), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 28, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 28, 40), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 30));
            Assert.AreEqual(new DateTime(2020, 12, 31, 23, 59, 40), t3);

            var t4 = occurance.Next(new DateTime(2021, 1, 1, 0, 0, 59));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t4);

            var t5 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 50));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t5);
        }

        [Test]
        public void Test_Every_60_Seconds()
        {
            var occurance = new EveryXSecondsOccurrence(60);
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 1, 0), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 28, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 29, 0), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 30));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t3);

            var t4 = occurance.Next(new DateTime(2021, 1, 1, 0, 30, 59));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 31, 0), t4);
        }

        [Test]
        public void Test_Every_120_Seconds()
        {
            var occurance = new EveryXSecondsOccurrence(120);
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 2, 0), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 28, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 30, 0), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 30));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t3);

            var t4 = occurance.Next(new DateTime(2021, 1, 1, 0, 30, 59));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 32, 0), t4);
        }
    }
}