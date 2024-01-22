using WpfManagerApp1.Services;
using WpfManagerApp1.Data;
using System.Windows;

namespace WpfManagerApp1.ViewModel
{
    internal class TestViewModel
    {
        public TestViewModel()
        {
            data = new BDMock();
            a = new WorksRouter(data);
            a.GetWorks()[0].Name = "Test";
            MessageBox.Show(a.GetWorks()[0].Name);
        }
        readonly IDataWorksProvider data;
        readonly WorksRouter a;

    }
}
