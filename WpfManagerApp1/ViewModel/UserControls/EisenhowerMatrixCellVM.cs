using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private Work _removedWorkItemVM;

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
        public ICommand WorkRecieveCommand { get; }        

        private bool CanWorkRecieveCommandExecute(object parameter)
        {
            return true;
        }
        private void OnWorkRecieveCommandExecuted(object parameter)
        {
            if (CellList.Contains(IncomingWorkItem)) return;
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

        public EisenhowerMatrixCellVM(MainWindowVM mainWindowVM, EisenhowerMatrixCell eisenhowerMatrixCell)
        {
            WorkRecieveCommand = new RelayCommand(OnWorkRecieveCommandExecuted, CanWorkRecieveCommandExecute);
            WorkRemovedCommandVM = new RelayCommand(OnWorkRemovedCommandExecuted, CanWorkRemovedCommandExecute);
            _mainWindowVM = mainWindowVM;
            _eisenhowerMatrixCell = eisenhowerMatrixCell;

            CellName = typeNameMap[eisenhowerMatrixCell];
            CellList = new ObservableCollection<Work>(_mainWindowVM.WorksRouter.GetWorks()
                .Where(x => x.EisenhowerMatrixCell == eisenhowerMatrixCell));
        }

    }
}
