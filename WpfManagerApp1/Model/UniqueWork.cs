﻿using System;

namespace WpfManagerApp1.Model
{
    public class UniqueWork : Work
    {
        private bool isTimeLimited;
        private DateTime deadLine;
        private int attemptsCount;

        public UniqueWork(int id) : base(id) { }

        public bool IsTimeLimited 
        { 
            get => isTimeLimited;
            set
            {
                isTimeLimited = value;
                OnWorkPropertyChanged();
            }
        }
        public DateTime DeadLine 
        { 
            get => deadLine;
            set
            {
                deadLine = value;
                OnWorkPropertyChanged();
            }
        }
        public int AttemptsCount 
        { 
            get => attemptsCount;
            set
            {
                attemptsCount = value;
                OnWorkPropertyChanged();
            }
        }
    }
}
