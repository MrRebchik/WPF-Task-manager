using System;
using System.Collections.Generic;
using System.Linq;
using WpfManagerApp1.Model;

namespace WpfManagerApp1.Services
{
    public enum SortFilters
    {
        ByImportance,
        ByImportanceDescending,
        ByImmediacy,
        ByImmediacyWithHabits,
        ByImmediacyDescending,
        ByImmediacyWithHabitsDescending,
        ByHabits,
    }
    public class WorksRouter
    {
        #region События

        public static event Action DataUpdated;
        public static event Action<Work> EntityEdited;
        public static event Action WorksRouterLoaded;
        public static event Action DataLoaded;

        #endregion

        #region Конструкторы

        public WorksRouter(IDataWorksProvider dataProvider)
        {
            AllWorksList = new List<Work>();
            DataProvider = dataProvider;
            LoadData();
            SortCommandsMap = new Dictionary<SortFilters, ListOperation>();
            InitializeSortCommandsMap();
            EntityEdited += DataProvider.EditWork;

            WorksRouterLoaded?.Invoke();

        }

        #endregion

        #region Свойства

        private List<Work> allWorksList;
        /// <summary>
        /// Список всех дел
        /// </summary>
        private List<Work> AllWorksList 
        { 
            get => allWorksList; 
            set => allWorksList = value; 
        }
        public Dictionary<SortFilters,ListOperation> SortCommandsMap { get; set; }
        private IDataWorksProvider DataProvider { get; set; }

        #endregion

        #region Методы

        private void LoadData()
        {
            if (DataProvider.GetWorks(out allWorksList))
            {
                DataLoaded?.Invoke();
            }
            else throw new Exception("Data has not been uploaded");
        }
        public void AddWork(Work item)
        {
            AllWorksList.Add(item);
            DataProvider.SaveWork(item);
            item.WorkPropertyChanged += DataProvider.EditWork;
            DataUpdated?.Invoke();
        }

        public void DeleteWork(Work item)
        {
            AllWorksList.Remove(item);
            DataUpdated?.Invoke();
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
            SortCommandsMap.Add(SortFilters.ByImmediacy, SortByImmediacy);
            SortCommandsMap.Add(SortFilters.ByImmediacyWithHabits, SortByImmediacyWithHabits);
            SortCommandsMap.Add(SortFilters.ByImmediacyDescending, SortByImmediacyDescending);
            SortCommandsMap.Add(SortFilters.ByImmediacyWithHabitsDescending, SortByImmediacyWithHabitsDescending);
            SortCommandsMap.Add(SortFilters.ByHabits, SortByHabits);
        }

        #endregion

        #region Делегаты

        public ListOperation SortByImportance = (work) => work.ToArray().OrderByDescending(n => n.Importance).ToList();
        public ListOperation SortByImportanceDescending = (work) => work.ToArray().OrderBy(n => n.Importance).ToList();
        public ListOperation SortByImmediacy = (work) =>
        {
            return work.ToArray().
            Where(n => n.GetType() == typeof(UniqueWork)).
            Select(n => (UniqueWork)n).
            OrderBy(n => n.DeadLine - DateTime.Now).
            Select(n => (Work)n).
            ToList();
        };
        public ListOperation SortByImmediacyWithHabits = (work) =>
        {
            return work.ToArray().
            Where(n => n.GetType() == typeof(UniqueWork)).
            Select(n => (UniqueWork)n).
            OrderBy(n => n.DeadLine - DateTime.Now).
            Union(work.Where(m => m.GetType() == typeof(RegularWork))).
            Select(n => (Work)n).
            ToList();
        };
        public ListOperation SortByImmediacyDescending = (work) =>
        {
            return work.ToArray().
            Where(n => n.GetType() == typeof(UniqueWork)).
            Select(n => (UniqueWork)n).
            OrderByDescending(n => n.DeadLine - DateTime.Now).
            Select(n => (Work)n).
            ToList();
        };
        public ListOperation SortByImmediacyWithHabitsDescending = (work) =>
        {
            return work.ToArray().
            Where(n => n.GetType() == typeof(UniqueWork)).
            Select(n => (UniqueWork)n).
            OrderByDescending(n => n.DeadLine - DateTime.Now).
            Union(work.Where(m => m.GetType() == typeof(RegularWork))).
            Select(n => (Work)n).
            ToList();
        };
        public ListOperation SortByHabits = (work) =>
        {
            return work.ToArray().
            Where(n => n.GetType() == typeof(RegularWork)).
            Select(n => (RegularWork)n).
            Where(n => n.IsHabit == true).
            Select(n => (Work)n).
            ToList();
        };


        public delegate List<Work> ListOperation(List<Work> work);
        #endregion
    }
}
