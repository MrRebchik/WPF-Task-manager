using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using WpfManagerApp1.ViewModel.Base;
using System.Collections.ObjectModel;
using WpfManagerApp1.Model;
using WpfManagerApp1.Infrastructure.Commands;

namespace WpfManagerApp1.ViewModel
{
    internal class MainWorkListVM : ViewModelBase
    {
        public ObservableCollection<Work> WorksCollection { get; set; }
        public Work SelectedWorkInFullList { get; set; }
    }
}
