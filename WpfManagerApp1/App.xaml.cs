using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfManagerApp1.ViewModel;

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
            MainWindowVM mainWindowVM = new MainWindowVM(mainWorkListVM);

            MainWindow = new MainWindow()
            {
                DataContext = mainWindowVM,
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
