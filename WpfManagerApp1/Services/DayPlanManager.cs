using System;
using System.Collections.Generic;
using WpfManagerApp1.Model;
using static WpfManagerApp1.Services.WorksRouter;

namespace WpfManagerApp1.Services
{

    public class DayPlanManager
    {
        #region События

        public static event Action DataUpdated;
        public static event Action DayPlanManagerLoaded;
        public static event Action DataLoaded;

        #endregion

        public DayPlanManager(DataProvider dataProvider)
        {
            AllDaysList = new List<DayPlan>();
            DataProvider = dataProvider;
            LoadData();

            DayPlanManagerLoaded?.Invoke();
        }

        #region Свойства

        private List<DayPlan> allDaysList;

        private List<DayPlan> AllDaysList { get => allDaysList; set => allDaysList = value; }
        public Dictionary<SortFilters, ListOperation> SortCommandsMap { get; set; }
        private IDataDayPlansProvider DataProvider { get; set; }

        #endregion

        #region Методы

        private void LoadData()
        {
            if (DataProvider.GetDaysPlans(out allDaysList))
            {
                DataLoaded?.Invoke();
            }
            else throw new Exception("Data has not been uploaded");
        }
        public void AddDayPlan(DayPlan item)
        {
            AllDaysList.Add(item);
            DataProvider.SaveDayPlan(item);
            item.DayPlanPropertyChanged += DataProvider.EditDayPlan;
            DataUpdated?.Invoke();
        }

        public void DeleteDayPlan(DayPlan item)
        {
            AllDaysList.Remove(item);
            DataUpdated?.Invoke();
        }

        /// <summary>
        /// Метод для получаения всех планов по дням
        /// </summary>
        public List<DayPlan> GetDayPlan()
        {
            return AllDaysList;
        }

        #endregion
    }
}
