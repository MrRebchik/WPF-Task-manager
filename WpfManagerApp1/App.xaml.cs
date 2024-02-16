using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfManagerApp1.ViewModel;
using WpfManagerApp1.ViewModel.UserControls;

namespace WpfManagerApp1
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWorkListVM mainWorkListVM = new MainWorkListVM();
            EisenhowerMatrixCellVM unimportantImmediatelyWorks = new EisenhowerMatrixCellVM() { CellName = "Неважные срочные"};

            MainWindowVM mainWindowVM = new MainWindowVM(mainWorkListVM, unimportantImmediatelyWorks)
            {
            };

            MainWindow = new MainWindow()
            {
                DataContext = mainWindowVM,
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
