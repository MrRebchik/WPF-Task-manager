using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfManagerApp1.Model
{
    public delegate void DayPlanPropertyChangedHandler(DayPlan item);
    public class DayPlan
    {
        private DateTime date;
        private DayOfWeek dayOfWeek;
        private List<Work> works;
        private int freeTimeAmuontInMinutes;

        DayPlanPropertyChangedHandler dayPlanPropertyChanged;
        public event DayPlanPropertyChangedHandler DayPlanPropertyChanged
        {
            add
            {
                if (dayPlanPropertyChanged?.GetInvocationList().Length == 0)
                    dayPlanPropertyChanged += value;
            }
            remove
            {
                dayPlanPropertyChanged -= value;
            }
        }

        public DayPlan(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
        public DateTime Date 
        { 
            get => date;
            set
            {
                date = value;
                OnDayPlanPropertyChanged();
            }
        }
        public DayOfWeek DayOfWeek 
        { 
            get => dayOfWeek;
            set
            {
                dayOfWeek = value;
                OnDayPlanPropertyChanged();
            }
        }
        public List<Work> Works 
        { 
            get => works;
            set
            {
                works = value;
                OnDayPlanPropertyChanged();
            }
        }
        public int FreeTimeAmuontInMinutes 
        { 
            get => freeTimeAmuontInMinutes;
            set
            {
                freeTimeAmuontInMinutes = value;
                OnDayPlanPropertyChanged();
            }
        }

        private void OnDayPlanPropertyChanged()
        {
            dayPlanPropertyChanged?.Invoke(this);
        }
    }
}
