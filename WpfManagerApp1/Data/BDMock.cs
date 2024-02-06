using System;
using System.Collections.Generic;
using System.Linq;
using WpfManagerApp1.Model;
using WpfManagerApp1.Services;

namespace WpfManagerApp1.Data
{
    public class BDMock : DataProvider
    {
        
        private readonly List<Work> works;
        private readonly List<DayPlan> dayPlans;
        public BDMock()
        {
            UniqueWork work = new UniqueWork(1)
            {
                Name = "Самое позднее - настроить гитару",
                Description = "Взять гиатру и тюнер и настроить гитару.",
                Importance = Importance.Low,
                DeadLine = new DateTime(2024, 9, 20),
                EisenhowerMatrixCell = EisenhowerMatrixCell.UnimportantUnmmediately,
                DurationInMinutes = 20,
                Completeness = CompleteStatus.Done,
            };
            UniqueWork work1 = new UniqueWork(2)
            {
                Name = "Второй очередности - помыть обувь",
                Description = "Взять тряпку и обувь и помыть её.",
                Importance = Importance.High,
                DeadLine = new DateTime(2024, 2, 29),
                EisenhowerMatrixCell = EisenhowerMatrixCell.UnimportantImmediately,
                DurationInMinutes = 50,
                Completeness = CompleteStatus.Frozen,
            };
            UniqueWork work2 = new UniqueWork(3)
            {
                Name = "Что делать в первую очередь - начать курсач",
                Description = "Найти задание, вникнуть в него, начать писать и вникать в материал.",
                Importance = Importance.Max,
                DeadLine = new DateTime(2024, 1, 15),
                EisenhowerMatrixCell = EisenhowerMatrixCell.ImportantImmediately,
                IsHighPriority = true,
                DurationInMinutes = 4 * 60,
            };
            RegularWork regular = new RegularWork(4)
            {
                Name = "Ложиться до 23 часов",
                Description ="Начать готовиться ко сну в 22:30, до этого закончить все дела.",
                Importance = Importance.Medium,
                EisenhowerMatrixCell = EisenhowerMatrixCell.ImportantUnimmediately,
                RepeatDays = new List<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Wednesday },
            };
            RegularWork regular1 = new RegularWork(5)
            {
                Name = "Стелить кровать",
                Description = "Ввести себе привычук стелить кровать сразу как проснулся",
                Importance = Importance.Low,
                EisenhowerMatrixCell = EisenhowerMatrixCell.UnimportantUnmmediately,
                IsHabit = true,
                DurationInMinutes = 3,
            };

            works = new List<Work>() { work, work1, work2, regular, regular1 };

            DayPlan d1 = new DayPlan(1)
            {
                Date = new DateTime(2024, 1, 1),
                FreeTimeAmuontInMinutes = 4 * 60,
                Works = new List<Work> { work, work1, regular1 }
            };
            DayPlan d2 = new DayPlan(2)
            {
                Date = new DateTime(2031, 2, 10),
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
        public override void DeleteWork(Work work)
        {
            works.Where(n => n.Id == work.Id).Select(n => works.Remove(n)); //ТЕСТИРОВАТЬ
        }

        public override void EditWork(Work work)
        {
            works.Where(n => n.Id == work.Id).Select(n => n = work);
        }

        public override bool GetWorks(out List<Work> list)
        {
            list = works;
            return true;
        }

        public override void SaveWork(Work work)
        {
            work.WorkPropertyChanged += EditWork;
            works.Add(work); // ТЕСТИРОВАТЬ
        }

        #endregion

        #region DayPlans

        public override void DeleteDayPlan(DayPlan dayPlan)
        {
            dayPlans.Where(n => n.Id == dayPlan.Id).Select(n => dayPlans.Remove(n)); //ТЕСТИРОВАТЬ
        }

        public override void EditDayPlan(DayPlan dayPlan)
        {
            dayPlans.Where(n => n.Id == dayPlan.Id).Select(n => n = dayPlan);
        }

        public override bool GetDaysPlans(out List<DayPlan> list)
        {
            list = dayPlans;
            return true;
        }

        public override void SaveDayPlan(DayPlan dayPlan)
        {
            dayPlan.DayPlanPropertyChanged += EditDayPlan;
            dayPlans.Add(dayPlan); // ТЕСТИРОВАТЬ
        }

        #endregion       

    }
}
