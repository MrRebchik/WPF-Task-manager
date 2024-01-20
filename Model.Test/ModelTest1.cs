using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    [TestClass]
    public class WorkerRouterTest1
    {
        [TestMethod]
        public void GetSortedByImportanceList1()
        {
            WorksRouter a = new WorksRouter();
            UniqueWork work = new UniqueWork();
            work.Importance = Importance.Low;
            UniqueWork work1 = new UniqueWork();
            work1.Importance = Importance.High;
            UniqueWork work2 = new UniqueWork();
            work2.Importance = Importance.Max;
            RegularWork regular = new RegularWork();
            regular.Importance = Importance.Medium;
            RegularWork regular1 = new RegularWork();
            regular1.Importance = Importance.Low;

            a.AddWork( work );
            a.AddWork( work1 );
            a.AddWork( work2 );
            a.AddWork( regular );
            a.AddWork( regular1 );

            Assert.AreEqual(Importance.Max, a.GetWorks(SortFilters.ByImportance)[0].Importance);
            Assert.AreEqual(Importance.High, a.GetWorks(SortFilters.ByImportance)[1].Importance);
            Assert.AreEqual(Importance.Medium, a.GetWorks(SortFilters.ByImportance)[2].Importance);
            Assert.AreEqual(Importance.Low, a.GetWorks(SortFilters.ByImportance)[3].Importance);
            Assert.AreEqual(Importance.Low, a.GetWorks(SortFilters.ByImportance)[4].Importance);
        }

        [TestMethod]
        public void GetSortedByImportanceDescendingList1()
        {
            WorksRouter a = new WorksRouter();
            UniqueWork work = new UniqueWork();
            work.Importance = Importance.Low;
            UniqueWork work1 = new UniqueWork();
            work1.Importance = Importance.High;
            UniqueWork work2 = new UniqueWork();
            work2.Importance = Importance.Max;
            RegularWork regular = new RegularWork();
            regular.Importance = Importance.Medium;
            RegularWork regular1 = new RegularWork();
            regular1.Importance = Importance.Low;

            a.AddWork(work);
            a.AddWork(work1);
            a.AddWork(work2);
            a.AddWork(regular);
            a.AddWork(regular1);

            Assert.AreEqual(Importance.Low, a.GetWorks(SortFilters.ByImportance)[3].Importance);
            Assert.AreEqual(Importance.Low, a.GetWorks(SortFilters.ByImportance)[4].Importance);
            Assert.AreEqual(Importance.Medium, a.GetWorks(SortFilters.ByImportance)[2].Importance);
            Assert.AreEqual(Importance.High, a.GetWorks(SortFilters.ByImportance)[1].Importance);
            Assert.AreEqual(Importance.Max, a.GetWorks(SortFilters.ByImportance)[0].Importance);
        }

        [TestMethod]
        public void DeleteWork()
        {
            WorksRouter a = new WorksRouter();
            UniqueWork work = new UniqueWork();
            work.Importance = Importance.Low;
            UniqueWork work1 = new UniqueWork();
            work1.Importance = Importance.High;
            UniqueWork work2 = new UniqueWork();
            work2.Importance = Importance.Max;
            RegularWork regular = new RegularWork();
            regular.Importance = Importance.Medium;
            RegularWork regular1 = new RegularWork();
            regular1.Importance = Importance.Low;

            a.AddWork(work);
            a.AddWork(work1);
            a.AddWork(work2);
            a.AddWork(regular);
            a.AddWork(regular1);

            a.DeleteWork(work);

            Assert.AreEqual(work1, a.GetWorks()[0]);
        }
    }
}
