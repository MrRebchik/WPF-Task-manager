using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfManagerApp1.Infrastructure.Commands;
using WpfManagerApp1.Model;
using WpfManagerApp1.Services;
using WpfManagerApp1.ViewModel.Base;

namespace WpfManagerApp1.ViewModel.UserControls
{
    internal class EisenhowerMatrixCellVM : ViewModelBase
    {
        Dictionary<EisenhowerMatrixCell, string> typeNameMap = new Dictionary<EisenhowerMatrixCell, string>() 
        {
            { EisenhowerMatrixCell.ImportantImmediately,"Важные срочные" },
            { EisenhowerMatrixCell.UnimportantImmediately,"Не важные срочные" },
            { EisenhowerMatrixCell.ImportantUnimmediately,"Важные не срочные" },
            { EisenhowerMatrixCell.UnimportantUnimmediately,"Не важные не срочные" },
        };

        private MainWindowVM _mainWindowVM;
        private EisenhowerMatrixCell _eisenhowerMatrixCell;
        private string cellName;
        private ObservableCollection<Work> cellList = new ObservableCollection<Work>();
        private Work _incomingWorkItem;
        private Work _removedWorkItemVM = null;

        public string CellName 
        { 
            get => cellName; 
            set => Set(ref cellName, value); 
        }
        public ObservableCollection<Work> CellList 
        { 
            get => cellList; 
            set => Set(ref cellList, value); 
        }
        public Work IncomingWorkItem
        {
            get { return _incomingWorkItem; }
            set { Set(ref _incomingWorkItem, value); }
        }
        public EisenhowerMatrixCell EisenhowerMatrixCell
        {
            get { return _eisenhowerMatrixCell; }
        }
        public ICommand WorkRecieveCommand { get; }        

        private bool CanWorkRecieveCommandExecute(object parameter)
        {
            return true;
        }
        private void OnWorkRecieveCommandExecuted(object parameter)
        {

            #region Проверка что этого задания нет в других ячейках

            foreach(var matrix in _mainWindowVM.Matrix)
            {
                if (matrix.CellList.Contains(IncomingWorkItem)) matrix.CellList.Remove(IncomingWorkItem);
            }

            #endregion

            if (CellList.Contains(IncomingWorkItem)) return; //чтобы нельзя было бесконечно добавлять одно и то же
            IncomingWorkItem.EisenhowerMatrixCell = _eisenhowerMatrixCell;
            CellList.Add(IncomingWorkItem);
        }

        public Work RemovedWorkItemVM
        {
            get { return _removedWorkItemVM; }
            set { Set(ref _removedWorkItemVM, value); }
        }
        public ICommand WorkRemovedCommandVM { get; }
        private bool CanWorkRemovedCommandExecute(object parameter)
        {
            return true;
        }
        private void OnWorkRemovedCommandExecuted(object parameter)
        {
            cellList.Remove(RemovedWorkItemVM);
        }

        public ICommand CheckIfDropDidComandVM { get; }

        private bool CanCheckIfDropDidCommandExecute(object parameter) => true;

        private void OnCheckIfDropDidCommandExecuted(object parameter) // происходит в источнике перетягивания
        {
            bool isSuccessfully = false;
            foreach (var matrix in _mainWindowVM.Matrix)
            {
                if(matrix.CellList.Contains(RemovedWorkItemVM)) isSuccessfully = true;
            }
            if(!isSuccessfully) cellList.Add(RemovedWorkItemVM);
        }

        public EisenhowerMatrixCellVM(MainWindowVM mainWindowVM, EisenhowerMatrixCell eisenhowerMatrixCell)
        {
            WorkRecieveCommand = new RelayCommand(OnWorkRecieveCommandExecuted, CanWorkRecieveCommandExecute);
            WorkRemovedCommandVM = new RelayCommand(OnWorkRemovedCommandExecuted, CanWorkRemovedCommandExecute);
            CheckIfDropDidComandVM = new RelayCommand(OnCheckIfDropDidCommandExecuted, CanCheckIfDropDidCommandExecute);
            _mainWindowVM = mainWindowVM;
            _eisenhowerMatrixCell = eisenhowerMatrixCell;

            CellName = typeNameMap[eisenhowerMatrixCell];
            CellList = new ObservableCollection<Work>(_mainWindowVM.WorksRouter.GetWorks()
                .Where(x => x.EisenhowerMatrixCell == eisenhowerMatrixCell).Where(x => x.Completeness == CompleteStatus.Active));
        }

    }
}
