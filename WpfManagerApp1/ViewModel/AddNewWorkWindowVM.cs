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

namespace WpfManagerApp1.ViewModel
{
    internal class AddNewWorkWindowVM : ViewModelBase
    {

        #region Свойства

        WorksRouter WorksRouter { get; set; }

        ObservableCollection<Work> WorksCollection { get; set; }

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

        public Dictionary<string, Type> TypesMap { get => typesMap; }
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
        public string SelectedType { get => selectedType; set => Set(ref selectedType, value); } 
        #endregion

        #endregion

        #region Команды

        public ICommand CreateNewWorkCommand { get; }
        private bool CanCreateNewWorkCommandExecute(object parameter) => true;
        private void OnCreateNewWorkCommandExecuted(object parameter)
        {
            if (string.IsNullOrWhiteSpace(WorkName) || string.IsNullOrEmpty(WorkDescription) || selectedImportance == null || SelectedType == null)
            {
                MessageBox.Show("Все поля для ввода должны быть заполенены");
                return;
            }
            if (selectedType == "Единичное задание") WorksRouter.AddWork(new UniqueWork(1) { Name = workName, Description = workDescription, Importance = importanceMap[selectedImportance] });
            else WorksRouter.AddWork(new RegularWork(1) { Name = workName, Description = workDescription, Importance = importanceMap[selectedImportance] });
            
            foreach (Window w in App.Current.Windows)
            {
                if(w is AddNewWorkWindow)
                    w.Close();
            }
        }

        #endregion

        public AddNewWorkWindowVM(WorksRouter worksRouter, ObservableCollection<Work> worksCollection)
        {
            WorksRouter = worksRouter;
            WorksCollection = worksCollection;

            CreateNewWorkCommand = new RelayCommand(OnCreateNewWorkCommandExecuted, CanCreateNewWorkCommandExecute);
            WorksCollection = worksCollection;
        }

    }
}
