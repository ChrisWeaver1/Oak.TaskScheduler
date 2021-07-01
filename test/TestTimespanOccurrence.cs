using System;
using NUnit.Framework;

namespace Oak.TaskScheduler.Test
{
    public class TestTimespanOccurrence
    {
        [SetUp]
        public void Setup()
        {
 
        }

        [Test]
        public void Timespan_1_Hour()
        {
            var occurance = new TimespanOccurrence(new TimeSpan(1, 0, 0));
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 30, 0));
            var t3 = occurance.Next(new DateTime(2020, 11, 1, 11, 59, 0));
            var t4 = occurance.Next(new DateTime(2020, 11, 1, 12, 1, 0));

            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 0, 0), t1);
            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 0, 0), t2);
            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 0, 0), t3);
            Assert.AreEqual(new DateTime(2020, 11, 1, 13, 0, 0), t4);
        }

        [Test]
        public void Timespan_1_Hour_Offset_30_Minutes()
        {
            var occurance = new TimespanOccurrence(new TimeSpan(1, 0, 0), new TimeSpan(0, 30, 0));
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 30, 0));
            var t3 = occurance.Next(new DateTime(2020, 11, 1, 11, 59, 0));
            var t4 = occurance.Next(new DateTime(2020, 11, 1, 12, 25, 0));
            var t5 = occurance.Next(new DateTime(2020, 11, 1, 12, 31, 0));

            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 30, 0), t1);
            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 30, 0), t2);
            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 30, 0), t3);
            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 30, 0), t4);
            Assert.AreEqual(new DateTime(2020, 11, 1, 13, 30, 0), t5);
        }

        [Test]
        public void Timespan_1_Minute_Offset_45_Seconds()
        {
            var occurance = new TimespanOccurrence(new TimeSpan(0, 1, 0), new TimeSpan(0, 0, 45));
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            var t2 = occurance.Next(new DateTime(2020, 11, 1, 11, 30, 45));
            var t3 = occurance.Next(new DateTime(2020, 11, 1, 11, 30, 46));
            var t4 = occurance.Next(new DateTime(2020, 11, 1, 12, 25, 0));

            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 0, 45), t1);
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 31, 45), t2);
            Assert.AreEqual(new DateTime(2020, 11, 1, 11, 31, 45), t3);
            Assert.AreEqual(new DateTime(2020, 11, 1, 12, 25, 45), t4);
        }

        [Test]
        public void Timespan_3_Days_12_Hours_Offset_6_Hours()
        {
            var occurance = new TimespanOccurrence(new TimeSpan(3, 12, 0, 0), new TimeSpan(6, 0, 0));
            
            var t1 = occurance.Next(new DateTime(2020, 11, 1, 11, 0, 0));
            var t2 = occurance.Next(new DateTime(2020, 11, 2, 11, 30, 45));
            var t3 = occurance.Next(new DateTime(2020, 11, 5, 17, 59, 59));

            Assert.AreEqual(new DateTime(2020, 11, 2, 6, 0, 0), t1);
            Assert.AreEqual(new DateTime(2020, 11, 5, 18, 0, 0), t2);
            Assert.AreEqual(new DateTime(2020, 11, 5, 18, 0, 0), t3);
        }

        [Test]
        public void Timespan_Throws_Offset_Too_High()
        {
            Assert.Throws<Exception>(() => { new TimespanOccurrence(new TimeSpan(0, 5, 0), new TimeSpan(0, 30, 0)); });
            Assert.Throws<Exception>(() => { new TimespanOccurrence(new TimeSpan(0, 5, 0), new TimeSpan(0, 5, 0)); });
            Assert.Throws<Exception>(() => { new TimespanOccurrence(new TimeSpan(0, 5, 0), new TimeSpan(0, 5, 1)); });

            Assert.DoesNotThrow(() => { new TimespanOccurrence(new TimeSpan(0, 5, 0), new TimeSpan(0, 4, 59)); });
        }
    }
}