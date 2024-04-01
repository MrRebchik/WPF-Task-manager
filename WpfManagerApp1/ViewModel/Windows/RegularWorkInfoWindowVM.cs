using WpfManagerApp1.Model;

namespace WpfManagerApp1.ViewModel.Windows
{
    internal class RegularWorkInfoWindowVM : WorkInfoWindowVM
    {
        public RegularWorkInfoWindowVM(RegularWork work, MainWindowVM parentVM) : base(work, parentVM) { }
    }
}
