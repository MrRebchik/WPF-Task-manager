﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfManagerApp1.Model;
using WpfManagerApp1.ViewModel.Base;

namespace WpfManagerApp1.ViewModel.Windows
{
    internal class RegularWorkInfoWindowVM : ViewModelBase
    {
        private Dictionary<EisenhowerMatrixCell, string> cellMap = new Dictionary<EisenhowerMatrixCell, string>()
        {
            {EisenhowerMatrixCell.ImportantImmediately, "Важное срочное" },
            {EisenhowerMatrixCell.UnimportantImmediately , "Не важное срочное" },
            {EisenhowerMatrixCell.ImportantUnimmediately , "Важное не срочное" },
            {EisenhowerMatrixCell.UnimportantUnimmediately , "Не важное не срочное" },
        };
        private Dictionary<Importance, string> importanceMap = new Dictionary<Importance, string>()
        {
            { Importance.Max , "Максимальная" },
            { Importance.High , "Высокая" },
            { Importance.Medium , "Нормальная" },
            { Importance.Low , "Низкая" },
        };
        private Dictionary<CompleteStatus, string> statusMap = new Dictionary<CompleteStatus, string>()
        {
            {CompleteStatus.Waiting , "Активное" },
            {CompleteStatus.Frozen , "Отложено" },
            {CompleteStatus.Done , "Выполнено" },
        };
        public List<string> CellList
        {
            get => EnumToList<EisenhowerMatrixCell>(cellMap);
            set => EnumToList<EisenhowerMatrixCell>(cellMap);
        }
        public List<string> ImportanceList
        {
            get => EnumToList<Importance>(importanceMap);
            set => EnumToList<Importance>(importanceMap);
        }
        public List<string> StatusList
        {
            get => EnumToList<CompleteStatus>(statusMap);
            set => EnumToList<CompleteStatus>(statusMap);
        }


        private RegularWork currentWork;


        public RegularWork CurrentWork
        {
            get => currentWork;
            set => Set(ref currentWork, value);
        }
        public string CurrentWorkType
        {
            get => "Повторяемое задание";
        }
        public string SelectedMatrixCell
        {
            get => cellMap[CurrentWork.EisenhowerMatrixCell];
            set
            {
                CurrentWork.EisenhowerMatrixCell = cellMap.Where(x => x.Value == value).Select(x => x.Key).FirstOrDefault();
                UpdateProperty?.Invoke(CurrentWork);
            }
        }
        public string SelectedWorkImportance
        {
            get => importanceMap[CurrentWork.Importance];
            set
            {
                CurrentWork.Importance = importanceMap.Where(x => x.Value == value).Select(x => x.Key).FirstOrDefault();
                UpdateProperty?.Invoke(CurrentWork);
            }
        }
        public string SelectedWorkStatus
        {
            get => statusMap[CurrentWork.Completeness];
            set
            {
                CurrentWork.Completeness = statusMap.Where(x => x.Value == value).Select(x => x.Key).FirstOrDefault();
                UpdateProperty?.Invoke(CurrentWork);
            }
        }
        public RegularWorkInfoWindowVM(RegularWork work)
        {
            this.currentWork = work;
        }

        private List<string> EnumToList<T>(Dictionary<T, string> map)
        {
            return map.Values.ToList();
        }
        public delegate void UpdatePropertyHandler(Work a);
        public event UpdatePropertyHandler UpdateProperty;
    }
}