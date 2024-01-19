using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WpfManagerApp1.Model;

namespace Model.Test
{
    [TestClass]
    public class ModelTest1
    {
        [TestMethod]
        public void RegularWorkSetRepeatDays1()
        {
            List<DayOfWeek> input = new List<DayOfWeek>() {DayOfWeek.Monday, DayOfWeek.Friday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Sunday };
            List<DayOfWeek> required = new List<DayOfWeek>() { DayOfWeek.Sunday, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Friday};
            RegularWork a = new RegularWork();

            a.RepeatDays = input;

            Assert.AreEqual(required.Count, a.RepeatDays.Count);
            Assert.AreEqual(4, a.RepeatDays.Count);
            Assert.AreEqual(required[0], a.RepeatDays[0]);
            Assert.AreEqual(required[1], a.RepeatDays[1]);
            Assert.AreEqual(required[2], a.RepeatDays[2]);
            Assert.AreEqual(required[3], a.RepeatDays[3]);
        }
    }
}
