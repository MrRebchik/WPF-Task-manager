using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfManagerApp1.Infrastructure.Commands;
using WpfManagerApp1.Model;
using WpfManagerApp1.ViewModel.Base;

namespace WpfManagerApp1.ViewModel.UserControls
{
    internal class EisenhowerMatrixCellVM : ViewModelBase
    {
        private string cellName;
        private ObservableCollection<Work> cellList = new ObservableCollection<Work>();

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

        public ICommand WorkRecieveCommand { get; }

        private void OnWorkRecieveCommandExecuted(object parameter)
        {

        }

        public EisenhowerMatrixCellVM()
        {
            WorkRecieveCommand = new RelayCommand(OnWorkRecieveCommandExecuted);
        }

    }
}
