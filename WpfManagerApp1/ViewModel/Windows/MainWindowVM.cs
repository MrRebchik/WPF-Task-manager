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
using WpfManagerApp1.ViewModel.UserControls;

namespace WpfManagerApp1.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        #region Другие VM
        
        private MainWorkListVM mainWorkListVM;
        public MainWorkListVM MainWorkListVM 
        { 
            get => mainWorkListVM; 
            set => Set(ref mainWorkListVM, value); 
        }

        public EisenhowerMatrixCellVM ImportantImmediatelyWorks { get; internal set; }
        public EisenhowerMatrixCellVM UnimportantImmediatelyWorks { get; internal set; }
        public EisenhowerMatrixCellVM ImportantUnimmediatelyWorks { get; internal set; }
        public EisenhowerMatrixCellVM UnimportantUnimmediatelyWorks { get; internal set; }


        #endregion

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

        public MainWindowVM(MainWorkListVM _mainWindowVM)
        {
            

            #region Связь с Model

            DataProvider = new BDMock();
            WorksRouter = new WorksRouter(DataProvider);
            DayPlanManager = new DayPlanManager(DataProvider);

            WorksCollection = new ObservableCollection<Work>(WorksRouter.GetWorks());


            #endregion
            
            this.MainWorkListVM = _mainWindowVM;

            ImportantImmediatelyWorks = new EisenhowerMatrixCellVM(this, EisenhowerMatrixCell.ImportantImmediately);
            UnimportantImmediatelyWorks = new EisenhowerMatrixCellVM(this, EisenhowerMatrixCell.UnimportantImmediately);
            ImportantUnimmediatelyWorks = new EisenhowerMatrixCellVM(this, EisenhowerMatrixCell.ImportantUnimmediately);
            UnimportantUnimmediatelyWorks = new EisenhowerMatrixCellVM(this, EisenhowerMatrixCell.UnimportantUnimmediately);

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
