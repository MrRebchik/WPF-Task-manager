using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfManagerApp1.Model;
using WpfManagerApp1.Services;

namespace WpfManagerApp1.Data
{
    internal class BDMock : IDataWorksProvider
    {
        UniqueWork a1;
        UniqueWork a2;
        RegularWork b1;

        List<Work> works;
        public BDMock()
        {
            a1 = new UniqueWork()
            {
                Name = "Один1",
            };
            a2 = new UniqueWork()
            {
                Name = "Один2",
            };
            b1 = new RegularWork()
            {
                Name = "Привычка1",
            };
            works = new List<Work>() { a1, a2, b1 };
        }
        public void DeleteWork(Work work)
        {
            throw new NotImplementedException();
        }

        public void EditWork(Work work)
        {
            throw new NotImplementedException();
        }

        public bool GetWorks(out List<Work> list)
        {

            list = works;
            return true;
        }

        public void SaveWork(Work work)
        {
            throw new NotImplementedException();
        }
    }
}
