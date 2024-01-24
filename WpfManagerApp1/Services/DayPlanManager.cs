using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfManagerApp1.Model;
using static WpfManagerApp1.Services.WorksRouter;

namespace WpfManagerApp1.Services
{
    public enum DaysSortFilter
    {

    }
    public class DayPlanManager
    {
        #region Свойства

        private List<DayPlan> AllDaysList { get; set; }
        public Dictionary<SortFilters, ListOperation> SortCommandsMap { get; set; }
        private IDataDayPlansProvider DataProvider { get; set; }

        #endregion

        #region Методы



        #endregion
    }
}
