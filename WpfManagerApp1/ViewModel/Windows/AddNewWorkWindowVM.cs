using System.Windows;
using System.Windows.Input;
using WpfManagerApp1.Infrastructure.Commands;
using WpfManagerApp1.ViewModel.Base;
using WpfManagerApp1.Services;
using WpfManagerApp1.Model;
using System.Collections.Generic;
using WpfManagerApp1.Views.Windows;
using System;
using WpfManagerApp1.Infrastructure.Utilities;

namespace WpfManagerApp1.ViewModel
{
    internal class AddNewWorkWindowVM : ViewModelBase
    {

        #region Свойства
        WorksRouter worksRouter { get; set; } // не понятно зачем это поле если потом ссылку получаю из mainWindowVM
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

        #region ImportanceList

        public List<string> ImportanceList { get => EnumToComboBox.EnumToList(EnumToComboBox.importanceMap); }

        #endregion

        #region TypesList

        public List<string> TypesList { get => EnumToComboBox.EnumToList(EnumToComboBox.typeMap); }

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
            get => selectedType == EnumToComboBox.typeMap[typeof(UniqueWork)];
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
            ValidateInput();

            Work createdWork;

            if (selectedType == EnumToComboBox.typeMap[typeof(UniqueWork)])
                createdWork = new UniqueWork(worksRouter.IncreaseLastID()) 
                { 
                    Name = workName, 
                    Description = workDescription, 
                    Importance = EnumToComboBox.GetEnumValueFromComboBox(EnumToComboBox.importanceMap, selectedImportance),
                    DeadLine = this.Deadline 
                };
            else 
                createdWork = new RegularWork(worksRouter.IncreaseLastID())
                { 
                    Name = workName, 
                    Description = workDescription, 
                    Importance = EnumToComboBox.GetEnumValueFromComboBox(EnumToComboBox.importanceMap,selectedImportance)
                };
            mainWindowVM.WorksRouter.AddWork(createdWork); 
            mainWindowVM.WorksCollection.Add(createdWork);
            CloseThisWindow();
        }

        #endregion

        public AddNewWorkWindowVM(WorksRouter worksRouter, MainWindowVM mainWindowVM)
        {
            this.worksRouter = worksRouter;
            this.mainWindowVM = mainWindowVM;

            CreateNewWorkCommand = new RelayCommand(OnCreateNewWorkCommandExecuted, CanCreateNewWorkCommandExecute);
        }

        void ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(WorkName) || selectedImportance == null || SelectedType == null)
            {
                MessageBox.Show("Все обязательные поля для ввода должны быть заполенены");
                return;
            }
        }
        void CloseThisWindow()
        {
            foreach (Window w in App.Current.Windows)
            {
                if (w is AddNewWorkWindow)
                    w.Close();
            }
        }

    }
}
