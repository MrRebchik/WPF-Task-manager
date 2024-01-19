using System;

namespace WpfManagerApp1.Model
{
    public class UniqueWork
    {
        public bool IsTimeLimited { get; set; } 
        public DateTime DeadLine { get; set; }
        public int AttemptsCount { get; set; }
    }
}
