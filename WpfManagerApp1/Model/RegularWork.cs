using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfManagerApp1.Model
{
    public class RegularWork : Work
    {
        private List<DayOfWeek> repeatDays;

        public List<DayOfWeek> RepeatDays 
        { 
            get => repeatDays; 
            set => repeatDays = value.ToArray().Distinct().OrderBy(n=>(int)n).ToList(); 
        }
        public bool IsHabit { get; set; }
    }
}
