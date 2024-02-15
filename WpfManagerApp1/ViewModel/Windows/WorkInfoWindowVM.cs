using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfManagerApp1.Model;
using WpfManagerApp1.ViewModel.Base;

namespace WpfManagerApp1.ViewModel
{
    internal class WorkInfoWindowVM : ViewModelBase
    {
        private Work currentWork;

        public Work CurrentWork 
        { 
            get => currentWork; 
            set => Set(ref currentWork,value); 
        }
        public string CurrentWorkType
        {
            get => CurrentWork.GetType().ToString();
        }

        public WorkInfoWindowVM(Work work)
        {
            this.currentWork = work;
        }
    }
}
