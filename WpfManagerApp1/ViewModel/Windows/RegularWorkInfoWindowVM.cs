using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfManagerApp1.Model;
using WpfManagerApp1.ViewModel.Base;
using WpfManagerApp1.ViewModel.UserControls;

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


        private RegularWork currentWork;
        private MainWindowVM parentVM;

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
            }
        }
        public RegularWorkInfoWindowVM(RegularWork work, MainWindowVM parentVM)
        {
            this.currentWork = work;
            this.parentVM = parentVM;
        }

        private void UpdateList()
        {
            parentVM.UpdateCollectionsContainsWork(CurrentWork);
        }
        private void ChangeCell(EisenhowerMatrixCell cell)
        {
            foreach (var col in parentVM.Matrix)
            {
                if (col.CellList.Contains(CurrentWork))
                {
                    col.CellList.Remove(CurrentWork);
                }
            }
            foreach ( var col in parentVM.Matrix )
            {
                if(col.EisenhowerMatrixCell == cell)
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
