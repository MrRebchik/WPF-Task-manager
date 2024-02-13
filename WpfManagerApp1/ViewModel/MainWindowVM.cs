using System.Windows;
using System.Windows.Input;
using WpfManagerApp1.Infrastructure.Commands;
using WpfManagerApp1.ViewModel.Base;
using WpfManagerApp1.Services;
using WpfManagerApp1.Data;
using WpfManagerApp1.Model;
using System.Collections.ObjectModel;
using WpfManagerApp1.Views.Windows;
using System.Linq;

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

        #region AddNewWorkCommand
        public ICommand AddNewWorkCommand { get; }
        private bool CanAddNewWorkCommandExecute(object parameter) => true;
        private void OnAddNewWorkCommandExecuted(object parameter)
        {
            AddNewWorkWindowVM addNewWorkWindowVM = new AddNewWorkWindowVM(WorksRouter, WorksCollection);

            AddNewWorkWindow addNewWorkWindow = new AddNewWorkWindow()
            {
                DataContext = addNewWorkWindowVM,
            };
            addNewWorkWindow.Show();
        }
        #endregion

        #region DeleteSelectedWorkCommand

        public ICommand DeleteSelectedWorkCommand { get; }

        private bool CanDeleteSelectedWorkCommandExecute(object parameter)

        {
            return SelectedWorkInFullList != null;
        }
        private void OnDeleteSelectedWorkCommandExecuted(object parameter)
        {
            WorksCollection.Remove(SelectedWorkInFullList);
        }

        #endregion

        

        #region Drag Drop

        #region WorkItemReceivedCommand

        public ICommand WorkItemReceivedCommand { get; }

        private bool CanWorkItemReceivedCommandExecute(object parameter) => true;

        private void OnWorkItemReceivedCommandExecuted(object parameter)
        {

        }

        #endregion 

        #endregion


        #endregion

        #region Свойства

        #region Связь с Model
        public DataProvider DataProvider { get; }
        public WorksRouter WorksRouter { get; set; }
        public DayPlanManager DayPlanManager { get; }
        #endregion

        #region Коллекции

        #region Коллекции дел

        public ObservableCollection<Work> WorksCollection { get; set; }

        public ObservableCollection<Work> ImportantImmediatelyWorksCollection { get; set; }
        public ObservableCollection<Work> ImportantUnimmediatelyWorksCollection { get; set; }
        public ObservableCollection<Work> UnimportantImmediatelyWorksCollection { get; set; }
        public ObservableCollection<Work> UnimportantUnimmediatelyWorksCollection { get; set; }


        #endregion

        public ObservableCollection<DayPlan> DayPlanCollection { get; set; }

        #endregion


        #region SelectedWorkInFullList - выбранный элемент в полном списке

        private Work selectedWorkInFullList;
        /// <summary>
        /// Выбранный элемент в полном списке
        /// </summary>
        public Work SelectedWorkInFullList { get => selectedWorkInFullList; set => Set(ref selectedWorkInFullList, value); } 

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
            WorksCollection.Add(WorksRouter.GetWorks()[WorksRouter.GetWorks().Count-1]);  // КОСТЫЛЬ
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
            ImportantImmediatelyWorksCollection = new ObservableCollection<Work>(WorksRouter.GetWorks()
                .Where(x => x.EisenhowerMatrixCell == EisenhowerMatrixCell.ImportantImmediately));
            UnimportantImmediatelyWorksCollection = new ObservableCollection<Work>(WorksRouter.GetWorks()
                .Where(x => x.EisenhowerMatrixCell == EisenhowerMatrixCell.UnimportantImmediately));
            ImportantUnimmediatelyWorksCollection = new ObservableCollection<Work>(WorksRouter.GetWorks()
                .Where(x => x.EisenhowerMatrixCell == EisenhowerMatrixCell.ImportantUnimmediately));
            UnimportantUnimmediatelyWorksCollection = new ObservableCollection<Work>(WorksRouter.GetWorks()
                .Where(x => x.EisenhowerMatrixCell == EisenhowerMatrixCell.UnimportantUnimmediately));

            #endregion

            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            AddNewWorkCommand = new RelayCommand(OnAddNewWorkCommandExecuted, CanAddNewWorkCommandExecute);
            DeleteSelectedWorkCommand = new RelayCommand(OnDeleteSelectedWorkCommandExecuted, CanDeleteSelectedWorkCommandExecute);
            WorkItemReceivedCommand = new RelayCommand(OnWorkItemReceivedCommandExecuted, CanWorkItemReceivedCommandExecute);

            #endregion

            #region Подписка на события

            WorksRouter.DataUpdated += OnWorkListUpdated;

            #endregion
        }
    }
}
