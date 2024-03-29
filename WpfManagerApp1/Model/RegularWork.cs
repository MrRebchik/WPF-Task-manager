﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfManagerApp1.Model
{
    public class RegularWork : Work
    {
        private List<DayOfWeek> repeatDays;
        private bool isHabit;

        public RegularWork(int id) : base(id) { }

        public List<DayOfWeek> RepeatDays 
        { 
            get => repeatDays;
            set
            {
                repeatDays = value.ToArray().Distinct().OrderBy(n => (int)n).ToList();
                base.OnWorkPropertyChanged();
            }
        }
        public bool IsHabit 
        { 
            get => isHabit;
            set
            {
                isHabit = value;
                base.OnWorkPropertyChanged();
            }
        }
    }
}
