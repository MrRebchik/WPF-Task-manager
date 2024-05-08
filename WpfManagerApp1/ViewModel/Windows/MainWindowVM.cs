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
using WpfManagerApp1.Infrastructure.Utilities;

namespace WpfManagerApp1.ViewModel
{
    internal class MainWindowVM : ViewModelBase
    {
        #region Другие VM
        
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


        #endregion

        #region Свойства

        #region Связь с Model
        public DataProvider DataProvider { get; }
        public WorksRouter WorksRouter { get; set; }
        public DayPlanRouter DayPlanManager { get; }
        #endregion

        #region Коллекции

        public ObservableCollection<Work> WorksCollection { get; set; }

        public ObservableCollection<DayPlan> DayPlanCollection { get; set; }

        public List<string> WorksFiltersList { get => EnumToComboBox.EnumToList(EnumToComboBox.worksFiltersMap); }

        private string selectedWorksFilter;
        public string SelectedWorksFilter
        {
            get => selectedWorksFilter;
            set 
            { 
                Set(ref selectedWorksFilter, value);
                WorksCollection = 
                    new ObservableCollection<Work>(
                        WorksRouter.GetWorks(EnumToComboBox.GetEnumValueFromComboBox(EnumToComboBox.worksFiltersMap, selectedWorksFilter)));
                OnPropertyChanged("WorksCollection");
            }
        }

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
            get { return "Текущая версия: " + Assembly.GetExecutingAssembly().GetName().Version.ToString(2); }
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

        #endregion

        public MainWindowVM()
        {
            #region Связь с Model

            //DataProvider = new BDMock();
            DataProvider = new DataBaseNoSQL();
            WorksRouter = new WorksRouter(DataProvider);
            DayPlanManager = new DayPlanRouter(DataProvider);

            WorksCollection = new ObservableCollection<Work>(WorksRouter.GetWorks());


            #endregion
            
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
            
            OpenWorkInfoWindowCommand = new RelayCommand(OnOpenWorkInfoWindowCommandExecuted, CanOpenWorkInfoWindowCommandExecute);

            #endregion


        }
    }
}
