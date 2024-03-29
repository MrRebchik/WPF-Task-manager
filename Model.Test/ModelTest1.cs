﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WpfManagerApp1.Data;
using WpfManagerApp1.Model;
using WpfManagerApp1.Services;

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
            RegularWork a = new RegularWork(1)
            {
                RepeatDays = input
            };

            Assert.AreEqual(required.Count, a.RepeatDays.Count);
            Assert.AreEqual(4, a.RepeatDays.Count);
            Assert.AreEqual(required[0], a.RepeatDays[0]);
            Assert.AreEqual(required[1], a.RepeatDays[1]);
            Assert.AreEqual(required[2], a.RepeatDays[2]);
            Assert.AreEqual(required[3], a.RepeatDays[3]);
        }

        [TestMethod]
        public void WorkPropertyChangedEvent()
        {
            BDMock bd = new BDMock();
            WorksRouter a = new WorksRouter(bd);
            List<Work> list;

            a.GetWorks()[0].Name= "A";
            bd.GetWorks(out list);

            Assert.AreEqual("A", list[0].Name);
        }


    }
    [TestClass]
    public class WorkerRouterTest1
    {
        [TestMethod]
        public void GetSortedByImportanceList1()
        {
            BDMock bd = new BDMock();
            WorksRouter a = new WorksRouter(bd);


            Assert.AreEqual(Importance.Max, a.GetWorks(SortFilters.ByImportance)[0].Importance);
            Assert.AreEqual(Importance.High, a.GetWorks(SortFilters.ByImportance)[1].Importance);
            Assert.AreEqual(Importance.Medium, a.GetWorks(SortFilters.ByImportance)[2].Importance);
            Assert.AreEqual(Importance.Low, a.GetWorks(SortFilters.ByImportance)[3].Importance);
            Assert.AreEqual(Importance.Low, a.GetWorks(SortFilters.ByImportance)[4].Importance);
        }

        [TestMethod]
        public void GetSortedByImportanceDescendingList1()
        {
            BDMock bd = new BDMock();
            WorksRouter a = new WorksRouter(bd);

            Assert.AreEqual(Importance.Low, a.GetWorks(SortFilters.ByImportance)[3].Importance);
            Assert.AreEqual(Importance.Low, a.GetWorks(SortFilters.ByImportance)[4].Importance);
            Assert.AreEqual(Importance.Medium, a.GetWorks(SortFilters.ByImportance)[2].Importance);
            Assert.AreEqual(Importance.High, a.GetWorks(SortFilters.ByImportance)[1].Importance);
            Assert.AreEqual(Importance.Max, a.GetWorks(SortFilters.ByImportance)[0].Importance);
        }

        [TestMethod]
        public void SortByImmediacy()
        {
            BDMock bd = new BDMock();
            WorksRouter a = new WorksRouter(bd);


            var b = a.GetWorks(SortFilters.ByImmediacy);

            Assert.AreEqual(3, b.Count);
            Assert.AreEqual("Что делать в первую очередь - начать курсач", b[0].Name);
            Assert.AreEqual("Второй очередности - помыть обувь", b[1].Name);
            Assert.AreEqual("Самое позднее - настроить гитару", b[2].Name);
        }

        [TestMethod]
        public void SortByImmediacyWithHabits()
        {
            BDMock bd = new BDMock();
            WorksRouter a = new WorksRouter(bd);


            var b = a.GetWorks(SortFilters.ByImmediacyWithHabits);

            Assert.AreEqual(5, b.Count);
            Assert.AreEqual("Что делать в первую очередь - начать курсач", b[0].Name);
            Assert.AreEqual("Второй очередности - помыть обувь", b[1].Name);
            Assert.AreEqual("Самое позднее - настроить гитару", b[2].Name);
            Assert.AreEqual("Ложиться до 23 часов", b[3].Name);
            Assert.AreEqual("Стелить кровать", b[4].Name);
        }

        [TestMethod]
        public void SortByImmediacyWithHabitsDescending()
        {
            BDMock bd = new BDMock();
            WorksRouter a = new WorksRouter(bd);


            var b = a.GetWorks(SortFilters.ByImmediacyWithHabitsDescending);

            Assert.AreEqual(5, b.Count);
            Assert.AreEqual("Самое позднее - настроить гитару", b[0].Name);
            Assert.AreEqual("Второй очередности - помыть обувь", b[1].Name);
            Assert.AreEqual("Что делать в первую очередь - начать курсач", b[2].Name);
            Assert.AreEqual("Ложиться до 23 часов", b[3].Name);
            Assert.AreEqual("Стелить кровать", b[4].Name);
        }

        [TestMethod]
        public void DeleteWork()
        {
            BDMock bd = new BDMock();
            WorksRouter a = new WorksRouter(bd);

            var work = a.GetWorks()[0];
            var work1 = a.GetWorks()[1];

            a.DeleteWork(work);

            Assert.AreEqual(work1, a.GetWorks()[0]);
        }
        [TestMethod]
        public void EditWork()
        {
            BDMock bd = new BDMock();
            WorksRouter a = new WorksRouter(bd);

            var work = a.GetWorks()[0];
            var work1 = a.GetWorks()[1];

            a.DeleteWork(work);

            Assert.AreEqual(work1, a.GetWorks()[0]);
        }


    }

    [TestClass]
    public class DataMockTest1
    {
        [TestMethod]
        public void EditWork1()
        {
            BDMock bd = new BDMock();
            List<Work> list;
            bd.GetWorks(out list);
            var work = list[0];

            work.Name = "New name";
            bd.EditWork(work);
            bd.GetWorks(out list);

            Assert.AreEqual ("New name", list[0].Name);
        }
    }
}
