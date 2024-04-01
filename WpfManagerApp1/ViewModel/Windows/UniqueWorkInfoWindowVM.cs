using System;
using WpfManagerApp1.Model;
using WpfManagerApp1.ViewModel.Windows;

namespace WpfManagerApp1.ViewModel
{
    internal class UniqueWorkInfoWindowVM : WorkInfoWindowVM
    {
        UniqueWork currentUniqueWork;
        public DateTime WorkDeadline
        {
            get
            {
                return currentUniqueWork.DeadLine;
            }
            set
            {
                currentUniqueWork.DeadLine = value;
                OnPropertyChanged(nameof(WorkDeadline));
                UpdateList();
            }
        }
        public UniqueWorkInfoWindowVM(UniqueWork work, MainWindowVM parentVM) : base(work, parentVM)
        {
            currentUniqueWork = work;
        }
    }
}
