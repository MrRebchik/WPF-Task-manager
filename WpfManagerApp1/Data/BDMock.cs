using System;
using System.Collections.Generic;
using System.Linq;
using WpfManagerApp1.Model;
using WpfManagerApp1.Services;

namespace WpfManagerApp1.Data
{
    public class BDMock : IDataWorksProvider
    {
        private readonly List<Work> works;
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
            foreach(var e in works)
            {
                e.WorkPropertyChanged += EditWork; // можно переделать в LINQ
            }
        }
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
            works.Add(work); // ТЕСТИРОВАТЬ
        }
    }
}
