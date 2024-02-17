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
        private Work selectedWork;

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

        public Work SelectedWork 
        { 
            get => selectedWork; 
            set => Set(ref selectedWork, value); 
        }
        public ICommand WorkRecieveCommand { get; }

        private void OnWorkRecieveCommandExecuted(object parameter)
        {
            
        }

        public EisenhowerMatrixCellVM(MainWindowVM mainWindowVM, EisenhowerMatrixCell eisenhowerMatrixCell)
        {
            WorkRecieveCommand = new RelayCommand(OnWorkRecieveCommandExecuted);
            _mainWindowVM = mainWindowVM;
            _eisenhowerMatrixCell = eisenhowerMatrixCell;

            CellName = typeNameMap[eisenhowerMatrixCell];
        }

    }
}
