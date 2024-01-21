using System.Collections.Generic;
using WpfManagerApp1.Model;

namespace WpfManagerApp1.Services
{
    public interface IDataWorksProvider
    {
        bool GetWorks(out List<Work> list);
        void SaveWork(Work work);
        void EditWork(Work work);
        void DeleteWork(Work work);
    }
}
