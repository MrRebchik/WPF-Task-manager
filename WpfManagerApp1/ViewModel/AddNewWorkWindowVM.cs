﻿using System.Windows;
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

namespace WpfManagerApp1.ViewModel
{
    internal class AddNewWorkWindowVM : ViewModelBase
    {

        #region Свойства

        WorksRouter WorksRouter { get; set; }

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

        #region ImportanceList
        private ObservableCollection<string> importanceList = new ObservableCollection<string>
        {
            "Низкая важность",
            "Обычная важность",
            "Высокая важность",
            "Максимальная важность",
        };

        public ObservableCollection<string> ImportanceList { get => importanceList; }
        #endregion

        private string selectedImportance;
        public string SelectedImportance { get => selectedImportance; set => Set( ref selectedImportance, value); }

        #endregion

        #region Команды

        public ICommand CreateNewWorkCommand { get; }
        private bool CanCreateNewWorkCommandExecute(object parameter) => true;
        private void OnCreateNewWorkCommandExecuted(object parameter)
        {
            WorksRouter.AddWork(new UniqueWork(1) { Name = workName, Description = workDescription });
            foreach(Window w in App.Current.Windows)
            {
                if(w is AddNewWorkWindow)
                    w.Close();
            }
        }

        #endregion

        public AddNewWorkWindowVM()
        {
            WorksRouter = MainWindowVM.WorksRouter;

            CreateNewWorkCommand = new RelayCommand(OnCreateNewWorkCommandExecuted, CanCreateNewWorkCommandExecute);
        }

    }
}
