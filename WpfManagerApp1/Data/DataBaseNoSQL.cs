using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfManagerApp1.Services;
using LiteDB;
using WpfManagerApp1.Model;
using WpfManagerApp1.Infrastructure.Commands;

namespace WpfManagerApp1.Data
{
    internal class DataBaseNoSQL : DataProvider
    {
        private List<Work> works;
        internal DataBaseNoSQL()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                // Получаем коллекцию
                var worksCollection = db.GetCollection<Work>("works");
                works = worksCollection.FindAll().ToList();
            }
        }
        public override bool GetWorks(out List<Work> list)
        {
            list = works;
            return true;
        }

        ////////////
        ///
        public override void DeleteDayPlan(DayPlan dayPlan)
        {
        }

        public override void DeleteWork(Work work)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var worksCollection = db.GetCollection<Work>("works");
                worksCollection.Delete(work.Id);
            }
            works.Remove(work);
        }

        public override void EditDayPlan(DayPlan dayPlan)
        {
        }

        public override void EditWork(Work work)
        {
        }

        public override bool GetDaysPlans(out List<DayPlan> list)
        {
            list = new List<DayPlan>();
            return true;
        }

        public override void SaveDayPlan(DayPlan dayPlan)
        {
        }

        public override void SaveWork(Work work)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var worksCollection = db.GetCollection<Work>("works");
                worksCollection.Insert(work);
            }
            works.Add(work);
        }
    }
}
