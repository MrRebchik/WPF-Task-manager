using System.Windows;
using System.Windows.Input;
using WpfManagerApp1.Infrastructure.Commands;
using WpfManagerApp1.ViewModel.Base;
using WpfManagerApp1.Services;
using WpfManagerApp1.Data;
using WpfManagerApp1.Model;
using System.Collections.ObjectModel;
using WpfManagerApp1.Views.Windows;

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

        public ICommand AddNewWorkCommand { get; }
        private bool CanAddNewWorkCommandExecute(object parameter) => true;
        private void OnAddNewWorkCommandExecuted(object parameter)
        {
            AddNewWorkWindow addNewWorkWindow = new AddNewWorkWindow();
            addNewWorkWindow.Show();
        }

        #endregion

        #region Свойства

        #region Связь с Model
        public DataProvider DataProvider { get; }
        static public WorksRouter WorksRouter { get; set; } // КОСТЫЛИ
        public DayPlanManager DayPlanManager { get; }
        #endregion

        #region Коллекции

        public ObservableCollection <Work> WorksCollection { get; set; }
        public ObservableCollection<DayPlan> DayPlanCollection { get; set; }

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

        #region Методы

        private void OnWorkListUpdated()
        {
            //WorksCollection = new ObservableCollection<Work>(WorksRouter.GetWorks());
            WorksCollection.Add(WorksRouter.GetWorks()[WorksRouter.GetWorks().Count-1]);
            OnPropertyChanged();
        }

        #endregion

        public MainWindowVM()
        {

            #region Связь с Model
            DataProvider = new BDMock();
            WorksRouter = new WorksRouter(DataProvider);
            DayPlanManager = new DayPlanManager(DataProvider);

            WorksCollection = new ObservableCollection<Work>(WorksRouter.GetWorks());
            #endregion

            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            AddNewWorkCommand = new RelayCommand(OnAddNewWorkCommandExecuted, CanAddNewWorkCommandExecute);

            #endregion

            #region Подписка на события

            WorksRouter.DataUpdated += OnWorkListUpdated;

            #endregion
        }
    }
}
