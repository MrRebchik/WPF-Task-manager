using System;
using System.Collections.Generic;
using System.Linq;
using WpfManagerApp1.Model;

namespace WpfManagerApp1.Services
{
    public enum SortFilters
    {
        ByImportance,
        ByImportanceDescending
    }
    public class WorksRouter
    {
        #region События

        public static event Action OnDataUpdated;
        public static event Action OnWorksRouterLoaded;

        #endregion

        #region Конструкторы

        public WorksRouter()
        {
            AllWorksList = new List<Work>();
            SortCommandsMap = new Dictionary<SortFilters, ListOperation>();
            InitializeSortCommandsMap();

            OnWorksRouterLoaded?.Invoke();
        }

        #endregion

        #region Свойства

        /// <summary>
        /// Список всех дел
        /// </summary>
        private List<Work> AllWorksList { get; set; }
        public Dictionary<SortFilters,ListOperation> SortCommandsMap { get; set; }

        #endregion

        #region Методы

        public void AddWork(Work item)
        {
            AllWorksList.Add(item);
            OnDataUpdated?.Invoke();
        }

        public void DeleteWork(Work item)
        {
            AllWorksList.Remove(item);
            OnDataUpdated?.Invoke();
        }

        /// <summary>
        /// Метод для получаения всех заданий
        /// </summary>
        public List<Work> GetWorks()
        {
            return AllWorksList;
        }

        /// <summary>
        /// Метод для получаения заданий по определенному фильтру из словаря LambdaMap
        /// </summary>
        /// <param name="comand">Делегат обрабатывающий список</param>
        /// <returns></returns>
        public List<Work> GetWorks(SortFilters comand)
        {
            return SortCommandsMap[comand](AllWorksList);
        }

        private void InitializeSortCommandsMap()
        {
            SortCommandsMap.Add(SortFilters.ByImportance, SortByImportance);
            SortCommandsMap.Add(SortFilters.ByImportanceDescending, SortByImportanceDescending);
        }

        #endregion

        #region Делегаты

        public ListOperation SortByImportance = (work) => work.ToArray().OrderByDescending(n => n.Importance).ToList();
        public ListOperation SortByImportanceDescending = (work) => work.ToArray().OrderBy(n => n.Importance).ToList();

        public delegate List<Work> ListOperation(List<Work> work);

        #endregion
    }
}
