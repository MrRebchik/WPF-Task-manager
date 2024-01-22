using System;

namespace WpfManagerApp1.Model
{
    public class UniqueWork : Work
    {
        public UniqueWork(int id) : base(id) { }

        public bool IsTimeLimited { get; set; } 
        public DateTime DeadLine { get; set; }
        public int AttemptsCount { get; set; }
    }
}
