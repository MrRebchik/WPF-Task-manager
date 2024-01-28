using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfManagerApp1.Infrastructure.Commands;
using WpfManagerApp1.ViewModel.Base;
using WpfManagerApp1.Services;
using WpfManagerApp1.Data;
using WpfManagerApp1.Model;
using System.Collections.ObjectModel;

namespace WpfManagerApp1.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }
        private bool CanCloseApplicationCommandExecute(object parameter) => true;
        private void OnCloseApplicationCommandExecuted(object parameter)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #endregion

        #region Свойства

        #region Связь с Model
        public DataProvider DataProvider { get; }
        public WorksRouter WorksRouter { get; }
        public DayPlanManager DayPlanManager { get; }
        #endregion

        #region Коллекции

        ObservableCollection <Work> WorksCollection { get; set; }
        ObservableCollection<DayPlan> DayPlanCollection { get; set; }

        #endregion

        #region Title - название окна
        private string _title = "Планировщик";
        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                Set(ref _title, value);
            }
        }

        #endregion

        #endregion

        public MainWindowVM()
        {

            #region Связь с Model
            DataProvider = new BDMock();
            WorksRouter = new WorksRouter(DataProvider);
            DayPlanManager = new DayPlanManager(DataProvider); 
            #endregion

            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion
        }
    }
}
