using System;

namespace WpfManagerApp1.Model
{
    public class UniqueWork : Work
    {
        public bool IsTimeLimited { get; set; } 
        public DateTime DeadLine { get; set; }
        public int AttemptsCount { get; set; }
    }
}
