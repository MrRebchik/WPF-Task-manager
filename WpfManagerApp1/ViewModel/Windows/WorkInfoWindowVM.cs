using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WpfManagerApp1.Model;
using WpfManagerApp1.ViewModel.Base;

namespace WpfManagerApp1.ViewModel
{
    internal class WorkInfoWindowVM : ViewModelBase
    {
        private Dictionary<Type, string> typesMap = new Dictionary<Type, string>()
        {
            {typeof(UniqueWork), "Одноразовое задание" },
            {typeof(RegularWork), "Повторяющееся задание" },
        };
        private Dictionary<EisenhowerMatrixCell, string> cellMap = new Dictionary<EisenhowerMatrixCell, string>()
        {
            {EisenhowerMatrixCell.ImportantImmediately, "Важное срочное" },
            {EisenhowerMatrixCell.UnimportantImmediately , "Не важное срочное" },
            {EisenhowerMatrixCell.ImportantUnimmediately , "Важное не срочное" },
            {EisenhowerMatrixCell.UnimportantUnimmediately , "Не важное не срочное" },
        };
        private Dictionary<Importance, string> importanceMap = new Dictionary<Importance, string>() 
        {
            { Importance.Max , "Максимальная" },
            { Importance.High , "Высокая" },
            { Importance.Medium , "Средняя" },
            { Importance.Low , "Низкая" },
        };
        private Dictionary<CompleteStatus, string> statusMap = new Dictionary<CompleteStatus, string>()
        {
            {CompleteStatus.Waiting , "Активное" },
            {CompleteStatus.Frozen , "Отложено" },
            {CompleteStatus.Done , "Выполнено" },
        }; 

        private Work currentWork;

        public Work CurrentWork 
        { 
            get => currentWork; 
            set => Set(ref currentWork,value); 
        }
        public string CurrentWorkType
        {
            get => typesMap[CurrentWork.GetType()];
        }

        public string MatrixCell
        {
            get => cellMap[CurrentWork.EisenhowerMatrixCell];
        }

        public string WorkImportance
        {
            get => importanceMap[CurrentWork.Importance];
        }
        public string WorkStatus
        {
            get => statusMap[CurrentWork.Completeness];
        }
        public DateTime WorkDeadline
        {
            get
            {
                if (CurrentWork.GetType() == typeof(UniqueWork))
                {
                    UniqueWork uniqueWork = (UniqueWork)CurrentWork;
                    return uniqueWork.DeadLine;
                }
                return new DateTime(0);
            }
        }

        public WorkInfoWindowVM(Work work)
        {
            this.currentWork = work;
        }
    }
}
