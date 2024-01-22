using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfManagerApp1.Model
{
    public class DayPlan
    {
        public DayPlan(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
        public DateTime Date { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public List<Work> Works { get; set; }
        public int FreeTimeAmuontInMinutes { get; set; }
    }
}
