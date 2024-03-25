using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfManagerApp1.Services;
using LiteDB;
using WpfManagerApp1.Model;

namespace WpfManagerApp1.Data
{
    internal class DataBaseNoSQL : DataProvider
    {
        private List<Work> works;
        //private string const WorkDBName = "";
        internal DataBaseNoSQL()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var worksCollection = db.GetCollection<Work>("works");
                works = worksCollection.FindAll().ToList();
            }
        }

        public override bool GetWorks(out List<Work> list)
        {
            list = works;
            return true;
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
        public override void EditWork(Work work)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var worksCollection = db.GetCollection<Work>("works");
                worksCollection.Update(work);
            }
            works.Remove(work);
        }
        public override void AddWork(Work work)
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
                var worksCollection = db.GetCollection<Work>("works");
                worksCollection.Insert(work);
            }
            works.Add(work);
        }

        public override void DeleteDayPlan(DayPlan dayPlan)
        {
        }
        public override void EditDayPlan(DayPlan dayPlan)
        {
        }
        public override bool GetDaysPlans(out List<DayPlan> list)
        {
            list = new List<DayPlan>();
            return true;
        }
        public override void AddDayPlan(DayPlan dayPlan)
        {
        }
    }
}
