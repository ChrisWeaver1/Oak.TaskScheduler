using System;
using NUnit.Framework;

namespace Oak.TaskScheduler.Test
{
    public class TestEveryXMinutesOccurrence
    {
        [SetUp]
        public void Setup()
        {
 
        }

        [Test]
        public void Test_Every_1_Minutes()
        {
            var occurance = new EveryXMinutesOccurrence(1);
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 1, 0), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 1, 0), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 30));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t3);
        }

        [Test]
        public void Test_Every_3_Minutes()
        {
            var occurance = new EveryXMinutesOccurrence(3);
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 3, 0), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 3, 0), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 30));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t3);

            var t4 = occurance.Next(new DateTime(2021, 1, 1, 0, 2, 59));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 3, 0), t4);
        }

        [Test]
        public void Test_Every_20_Minutes()
        {
            var occurance = new EveryXMinutesOccurrence(20);
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 20, 0), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 28, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 40, 0), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 30));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t3);

            var t4 = occurance.Next(new DateTime(2021, 1, 1, 0, 0, 59));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 20, 0), t4);
        }

        [Test]
        public void Test_Every_60_Minutes()
        {
            var occurance = new EveryXMinutesOccurrence(60);
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 0, 0), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 28, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 0, 0), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 30));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t3);

            var t4 = occurance.Next(new DateTime(2021, 1, 1, 0, 30, 59));
            Assert.AreEqual(new DateTime(2021, 1, 1, 1, 0, 0), t4);
        }

        [Test]
        public void Test_Every_120_Minutes()
        {
            var occurance = new EveryXMinutesOccurrence(120);
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 0, 0), t1);

            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 28, 30));
            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 0, 0), t2);

            var t3 = occurance.Next(new DateTime(2020, 12, 31, 23, 59, 30));
            Assert.AreEqual(new DateTime(2021, 1, 1, 0, 0, 0), t3);

            var t4 = occurance.Next(new DateTime(2021, 1, 1, 0, 30, 59));
            Assert.AreEqual(new DateTime(2021, 1, 1, 2, 0, 0), t4);
        }
    }
}