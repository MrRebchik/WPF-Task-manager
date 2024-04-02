using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfManagerApp1.Model;
using WpfManagerApp1.ViewModel.Base;

namespace WpfManagerApp1.ViewModel.Windows
{
    internal class WorkInfoWindowVM : ViewModelBase
    {
        private Dictionary<Type, string> typeMap = new Dictionary<Type, string>()
        {
            {typeof(UniqueWork), "Одноразовое задание"},
            {typeof(RegularWork), "Повторяемое задание"},
        };
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
            {CompleteStatus.Active , "Активное" },
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


        private Work currentWork;
        private MainWindowVM parentVM;

        public Work CurrentWork
        {
            get => currentWork;
            set => Set(ref currentWork, value);
        }

        private string currentWorkType;
        public string CurrentWorkType
        {
            get => typeMap[CurrentWork.GetType()];
            set => Set(ref currentWorkType, value);
        }
        public string SelectedMatrixCell
        {
            get => cellMap[CurrentWork.EisenhowerMatrixCell];
            set
            {
                ChangeCell(cellMap.Where(x => x.Value == value).Select(x => x.Key).FirstOrDefault());
                CurrentWork.EisenhowerMatrixCell = GetValueFromMap(cellMap, value);
                UpdateList();
            }
        }
        public string SelectedWorkImportance
        {
            get => importanceMap[CurrentWork.Importance];
            set
            {
                CurrentWork.Importance = GetValueFromMap(importanceMap, value);
                UpdateList();
            }
        }
        public string SelectedWorkStatus
        {
            get => statusMap[CurrentWork.Completeness];
            set
            {
                CurrentWork.Completeness = GetValueFromMap(statusMap, value);
                UpdateList();
            }
        }

        public WorkInfoWindowVM(Work work, MainWindowVM parentVM)
        {
            this.currentWork = work;
            this.parentVM = parentVM;
        }
        public void UpdateList()
        {
            parentVM.UpdateCollectionsContainsWork(CurrentWork);
        }

        private void ChangeCell(EisenhowerMatrixCell cell)
        {
            RemoveWorkFromCells();
            AddWorkToCell(cell);
        }
        private void RemoveWorkFromCells()
        {
            foreach (var col in parentVM.Matrix)
            {
                if (col.CellList.Contains(CurrentWork))
                {
                    col.CellList.Remove(CurrentWork);
                }
            }
        }
        private void AddWorkToCell(EisenhowerMatrixCell cell)
        {
            foreach (var col in parentVM.Matrix)
            {
                if (col.EisenhowerMatrixCell == cell)
                {
                    col.CellList.Add(CurrentWork);
                }
            }
        }
        private List<string> EnumToList<T>(Dictionary<T, string> map)
        {
            return map.Values.ToList();
        }
        private T GetValueFromMap<T>(Dictionary<T, string> map, string value)
        {
            return map.Where(x => x.Value == value).Select(x => x.Key).FirstOrDefault();
        }
    }
}
