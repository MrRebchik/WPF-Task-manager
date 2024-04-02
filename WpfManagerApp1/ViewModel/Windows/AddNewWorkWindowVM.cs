using System.Windows;
using System.Windows.Input;
using WpfManagerApp1.Infrastructure.Commands;
using WpfManagerApp1.ViewModel.Base;
using WpfManagerApp1.Services;
using WpfManagerApp1.Data;
using WpfManagerApp1.Model;
using System.Collections.ObjectModel;
using System.Security.Policy;
using System.Collections.Generic;
using WpfManagerApp1.Views.Windows;
using System;
using System.Xml.Linq;
using System.Dynamic;
using System.Windows.Navigation;

namespace WpfManagerApp1.ViewModel
{
    internal class AddNewWorkWindowVM : ViewModelBase
    {

        #region Свойства

        WorksRouter WorksRouter { get; set; }

        MainWindowVM mainWindowVM { get; set; }

        #region Title - название окнa
        private string title = "Создание нового задания";
        
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }
        #endregion

        #region WorkName
        private string workName;
        public string WorkName { get => workName; set => Set(ref workName, value); }
        #endregion

        #region WorkDescription
        private string workDescription;

        public string WorkDescription { get => workDescription; set => Set(ref workDescription, value); }
        #endregion

        #region ImportanceMap
        private Dictionary<string, Importance> importanceMap = new Dictionary<string, Importance>
        {
            {"Низкая важность" , Importance.Low },
            {"Обычная важность" , Importance.Medium },
            {"Высокая важность" , Importance.High },
            {"Максимальная важность" , Importance.Max },
        };
        
        public Dictionary<string, Importance> ImportanceMap { get => importanceMap;}
        #endregion

        #region importanceList
        private List<string> importanceList = new List<string>
        {
            "Низкая важность",
            "Обычная важность",
            "Высокая важность",
            "Максимальная важность",
        };

        public List<string> ImportanceList { get => importanceList; }

        #endregion

        #region TypesMap
        private Dictionary<string, Type> typesMap = new Dictionary<string, Type>
        {
            {"Единичное задание" , typeof(UniqueWork) },
            {"Повторяющееся задание" , typeof(RegularWork)},
        };
        #endregion

        #region importanceList
        private List<string> typesList = new List<string>
        {
            "Единичное задание",
            "Повторяющееся задание",
        };

        public List<string> TypesList { get => typesList; }

        #endregion

        #region Selected Importance, Type
        private string selectedImportance;
        public string SelectedImportance { get => selectedImportance; set => Set(ref selectedImportance, value); }

        private string selectedType;
        public string SelectedType 
        { 
            get => selectedType; 
            set { Set(ref selectedType, value); OnPropertyChanged("IsUniqueWorkType"); } 
        }
        public bool IsUniqueWorkType
        {
            get => selectedType == "Единичное задание";
        }

        private DateTime deadline = DateTime.Today;
        public DateTime Deadline
        {
            get => deadline.AddDays(7);
            set => Set(ref deadline, value);
        }
        #endregion

        #endregion

        #region Команды

        public ICommand CreateNewWorkCommand { get; }
        private bool CanCreateNewWorkCommandExecute(object parameter) => true;
        private void OnCreateNewWorkCommandExecuted(object parameter)
        {
            if (string.IsNullOrWhiteSpace(WorkName) || selectedImportance == null || SelectedType == null)
            {
                MessageBox.Show("Все обязательные поля для ввода должны быть заполенены");
                return;
            }
            Work createdWork;

            if (selectedType == "Единичное задание") 
                createdWork = new UniqueWork(WorksRouter.IncreaseLastID()) 
                { Name = workName, Description = workDescription, Importance = importanceMap[selectedImportance], DeadLine= this.Deadline };
            else 
                createdWork = new RegularWork(WorksRouter.IncreaseLastID())
                { Name = workName, Description = workDescription, Importance = importanceMap[selectedImportance] };
            mainWindowVM.WorksRouter.AddWork(createdWork);
            mainWindowVM.WorksCollection.Add(createdWork);
            foreach (Window w in App.Current.Windows)
            {
                if(w is AddNewWorkWindow)
                    w.Close();
            }
        }

        #endregion

        public AddNewWorkWindowVM(WorksRouter worksRouter, MainWindowVM mainWindowVM)
        {
            WorksRouter = worksRouter;
            this.mainWindowVM = mainWindowVM;
            CreateNewWorkCommand = new RelayCommand(OnCreateNewWorkCommandExecuted, CanCreateNewWorkCommandExecute);
        }

    }
}
