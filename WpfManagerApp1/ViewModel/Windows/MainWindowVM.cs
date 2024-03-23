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
using System.Windows.Documents;
using System.Collections.Generic;
using WpfManagerApp1.ViewModel.Windows;
using System.Reflection;

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
        public List<EisenhowerMatrixCellVM> Matrix { get; set; }
        public EisenhowerMatrixCellVM ImportantImmediatelyWorksVM { get; internal set; }
        public EisenhowerMatrixCellVM UnimportantImmediatelyWorksVM { get; internal set; }
        public EisenhowerMatrixCellVM ImportantUnimmediatelyWorksVM { get; internal set; }
        public EisenhowerMatrixCellVM UnimportantUnimmediatelyWorksVM { get; internal set; }


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
            AddNewWorkWindowVM addNewWorkWindowVM = new AddNewWorkWindowVM(WorksRouter, this);

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
            var workToDelete = SelectedWorkInFullList;
            WorksCollection.Remove(workToDelete);
            WorksRouter.DeleteWork(workToDelete);
        }

        #endregion

        #region OpenWorkInfoWindowCommand
        public ICommand OpenWorkInfoWindowCommand { get; }
        private bool CanOpenWorkInfoWindowCommandExecute(object parametr)
        {
            return SelectedWorkInFullList != null; ;
        }
        private void OnOpenWorkInfoWindowCommandExecuted(object parametr)
        {
            if (SelectedWorkInFullList.GetType() == typeof(UniqueWork))
            {
                UniqueWorkInfoWindowVM _uniqueWorkInfoWindowVM = new UniqueWorkInfoWindowVM((UniqueWork)SelectedWorkInFullList, this);
                UniqueWorkInfoWindow _uniqueWorkInfoWindow = new UniqueWorkInfoWindow()
                {
                    DataContext = _uniqueWorkInfoWindowVM,
                };
                _uniqueWorkInfoWindow.Show();
            }
            if (SelectedWorkInFullList.GetType() == typeof(RegularWork))
            {
                RegularWorkInfoWindowVM _regularWorkInfoWindowVM = new RegularWorkInfoWindowVM((RegularWork)SelectedWorkInFullList, this);
                RegularWorkInfoWindow _regularWorkInfoWindow = new RegularWorkInfoWindow()
                {
                    DataContext = _regularWorkInfoWindowVM,
                };
                _regularWorkInfoWindow.Show();
            }
        } 
        #endregion

        #region Drag Drop

        #region WorkItemReceivedCommand

        //public ICommand WorkItemReceivedCommand { get; }

        //private bool CanWorkItemReceivedCommandExecute(object parameter) => true;

        //private void OnWorkItemReceivedCommandExecuted(object parameter)
        //{

        //}

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

        public ObservableCollection<Work> WorksCollection { get; set; }

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

        public string CurrentVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }
        #endregion

        #region Методы

        public void UpdateCollectionsContainsWork(Work work)
        {
            RewriteWorkInList(work);
        }
        private void RewriteWorkInList(Work item)
        {
            int index = WorksCollection.IndexOf(item);
            WorksCollection.RemoveAt(index);
            WorksCollection.Insert(index, item);
        }
        public void ChangeEisenhowerMatrixCell(Work work, EisenhowerMatrixCell newCell)
        {
            //возможно заснуть этот метод в команду удаления
            //в команде удаления проверять DropAllow у объекта над которым находится

        }

        #endregion

        public MainWindowVM(MainWorkListVM _mainWindowVM)
        {
            #region Связь с Model

            //DataProvider = new BDMock();
            DataProvider = new DataBaseNoSQL();
            WorksRouter = new WorksRouter(DataProvider);
            DayPlanManager = new DayPlanManager(DataProvider);

            WorksCollection = new ObservableCollection<Work>(WorksRouter.GetWorks());


            #endregion
            
            this.MainWorkListVM = _mainWindowVM;
            
            ImportantImmediatelyWorksVM = new EisenhowerMatrixCellVM(this, EisenhowerMatrixCell.ImportantImmediately);
            UnimportantImmediatelyWorksVM = new EisenhowerMatrixCellVM(this, EisenhowerMatrixCell.UnimportantImmediately);
            ImportantUnimmediatelyWorksVM = new EisenhowerMatrixCellVM(this, EisenhowerMatrixCell.ImportantUnimmediately);
            UnimportantUnimmediatelyWorksVM = new EisenhowerMatrixCellVM(this, EisenhowerMatrixCell.UnimportantUnimmediately);

            Matrix = new List<EisenhowerMatrixCellVM>()
            {
                ImportantImmediatelyWorksVM,
                UnimportantImmediatelyWorksVM,
                ImportantUnimmediatelyWorksVM,
                UnimportantUnimmediatelyWorksVM,
            };
            

            #region Команды

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            AddNewWorkCommand = new RelayCommand(OnAddNewWorkCommandExecuted, CanAddNewWorkCommandExecute);
            DeleteSelectedWorkCommand = new RelayCommand(OnDeleteSelectedWorkCommandExecuted, CanDeleteSelectedWorkCommandExecute);
            //WorkItemReceivedCommand = new RelayCommand(OnWorkItemReceivedCommandExecuted, CanWorkItemReceivedCommandExecute);
            OpenWorkInfoWindowCommand = new RelayCommand(OnOpenWorkInfoWindowCommandExecuted, CanOpenWorkInfoWindowCommandExecute);

            #endregion


        }
    }
}
