using System;
using System.Collections.Generic;
using System.Linq;
using WpfManagerApp1.Model;
using WpfManagerApp1.Services;

namespace WpfManagerApp1.Data
{
    public class BDMock : IDataWorksProvider, IDataDayPlansProvider
    {
        
        private readonly List<Work> works;
        private readonly List<DayPlan> dayPlans;
        public BDMock()
        {
            UniqueWork work = new UniqueWork(1)
            {
                Name = "Last",
                Importance = Importance.Low,
                DeadLine = new DateTime(2026, 5, 20),
            };
            UniqueWork work1 = new UniqueWork(2)
            {
                Name = "Next",
                Importance = Importance.High,
                DeadLine = new DateTime(2025, 5, 20),
            };
            UniqueWork work2 = new UniqueWork(3)
            {
                Name = "Previous",
                Importance = Importance.Max,
                DeadLine = new DateTime(2016, 5, 20),
            };
            RegularWork regular = new RegularWork(4)
            {
                Name = "Routine0",
                Importance = Importance.Medium
            };
            RegularWork regular1 = new RegularWork(5)
            {
                Name = "Routine1",
                Importance = Importance.Low
            };

            works = new List<Work>() { work, work1, work2, regular, regular1 };

            DayPlan d1 = new DayPlan(1)
            {
                Date = new DateTime(2031, 1, 1),
                FreeTimeAmuontInMinutes = 4 * 60,
                Works = new List<Work> { work, work1, regular1 }
            };
            DayPlan d2 = new DayPlan(2)
            {
                Date = new DateTime(2031, 1, 1),
                FreeTimeAmuontInMinutes = 6 * 60,
                Works = new List<Work> { work2, regular }
            };

            dayPlans = new List<DayPlan>() { d1, d2 };

            foreach(var e in works)
            {
                e.WorkPropertyChanged += EditWork; // можно переделать в LINQ
            }
            foreach (var e in dayPlans)
            {
                e.DayPlanPropertyChanged += EditDayPlan; // можно переделать в LINQ
            }
        }

        #region Works
        public void DeleteWork(Work work)
        {
            works.Where(n => n.Id == work.Id).Select(n => works.Remove(n)); //ТЕСТИРОВАТЬ
        }

        public void EditWork(Work work)
        {
            works.Where(n => n.Id == work.Id).Select(n => n = work);
        }

        public bool GetWorks(out List<Work> list)
        {
            list = works;
            return true;
        }

        public void SaveWork(Work work)
        {
            work.WorkPropertyChanged += EditWork;
            works.Add(work); // ТЕСТИРОВАТЬ
        }

        #endregion

        #region DayPlans

        public void DeleteDayPlan(DayPlan dayPlan)
        {
            dayPlans.Where(n => n.Id == dayPlan.Id).Select(n => dayPlans.Remove(n)); //ТЕСТИРОВАТЬ
        }

        public void EditDayPlan(DayPlan dayPlan)
        {
            dayPlans.Where(n => n.Id == dayPlan.Id).Select(n => n = dayPlan);
        }

        public bool GetDaysPlans(out List<DayPlan> list)
        {
            list = dayPlans;
            return true;
        }

        public void SaveDayPlan(DayPlan dayPlan)
        {
            dayPlan.DayPlanPropertyChanged += EditDayPlan;
            dayPlans.Add(dayPlan); // ТЕСТИРОВАТЬ
        }

        #endregion       

    }
}
