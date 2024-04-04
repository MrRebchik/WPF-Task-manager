using System;
using System.Collections.Generic;
using WpfManagerApp1.Infrastructure.Utilities;
using WpfManagerApp1.Model;
using WpfManagerApp1.ViewModel.Base;

namespace WpfManagerApp1.ViewModel.Windows
{
    internal class WorkInfoWindowVM : ViewModelBase
    {
        private Dictionary<Type, string> typeMap = EnumToComboBox.typeMap;
        private Dictionary<EisenhowerMatrixCell, string> cellMap = EnumToComboBox.cellMap;
        private Dictionary<Importance, string> importanceMap = EnumToComboBox.importanceMap;
        private Dictionary<CompleteStatus, string> statusMap = EnumToComboBox.statusMap;
        public List<string> CellList
        {
            get => EnumToList(cellMap);
            set => EnumToList(cellMap);
        }
        public List<string> ImportanceList
        {
            get => EnumToList(importanceMap);
            set => EnumToList(importanceMap);
        }
        public List<string> StatusList
        {
            get => EnumToList(statusMap);
            set => EnumToList(statusMap);
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
                ChangeCell(GetEnumValueFromComboBox(cellMap, value));
                CurrentWork.EisenhowerMatrixCell = GetEnumValueFromComboBox(cellMap, value);
                UpdateList();
            }
        }
        public string SelectedWorkImportance
        {
            get => importanceMap[CurrentWork.Importance];
            set
            {
                CurrentWork.Importance = GetEnumValueFromComboBox(importanceMap, value);
                UpdateList();
            }
        }
        public string SelectedWorkStatus
        {
            get => statusMap[CurrentWork.Completeness];
            set
            {
                CurrentWork.Completeness = GetEnumValueFromComboBox(statusMap, value);
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
            return EnumToComboBox.EnumToList(map);
        }
        private T GetEnumValueFromComboBox<T>(Dictionary<T, string> map, string value)
        {
            return EnumToComboBox.GetEnumValueFromComboBox(map, value);
        }
    }
}
