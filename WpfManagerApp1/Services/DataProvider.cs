using System;
using System.Collections.Generic;
using WpfManagerApp1.Model;

namespace WpfManagerApp1.Services
{
    public abstract class DataProvider : IDataWorksProvider, IDataDayPlansProvider
    {
        public virtual void DeleteDayPlan(DayPlan dayPlan)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteWork(Work work)
        {
            throw new NotImplementedException();
        }

        public virtual void EditDayPlan(DayPlan dayPlan)
        {
            throw new NotImplementedException();
        }

        public virtual void EditWork(Work work)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetDaysPlans(out List<DayPlan> list)
        {
            throw new NotImplementedException();
        }

        public virtual bool GetWorks(out List<Work> list)
        {
            throw new NotImplementedException();
        }

        public virtual void SaveDayPlan(DayPlan dayPlan)
        {
            throw new NotImplementedException();
        }

        public virtual void SaveWork(Work work)
        {
            throw new NotImplementedException();
        }
    }
}
