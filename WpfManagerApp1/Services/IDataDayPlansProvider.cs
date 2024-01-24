using System.Collections.Generic;
using WpfManagerApp1.Model;

namespace WpfManagerApp1.Services
{
    public interface IDataDayPlansProvider
    {
        bool GetDaysPlans(out List<DayPlan> list);
        void SaveDayPlan(DayPlan dayPlan);
        void EditDayPlan(DayPlan dayPlan);
        void DeleteDayPlan(DayPlan dayPlan);
    }
}
