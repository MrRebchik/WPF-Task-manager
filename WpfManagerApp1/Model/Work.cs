using System;

namespace WpfManagerApp1.Model
{
    public enum CompleteStatus
    {
        Done,Waiting,Frozen
    }
    public enum Importance
    {
        Low,
        Medium,
        High,
        Max
    }
    public enum EisenhowerMatrixCell
    {
        ImportantImmediately,
        ImportantUnimmediately,
        UnimportantImmediately,
        UnimportantUnmmediately
    }
    public abstract class Work
    {
        public string Name { get; set; } = "Укажите название";
        public string Description { get; set; } = "Добавить описание";
        public CompleteStatus Completeness { get; set; }
        public Importance Importance { get; set; }
        /// <summary>
        /// Примерное время затрачиваемое на задание (мин.)
        /// </summary>
        public int DurationInMinutes { get; set; }
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Устанавливается true для дел которые получают особый приоритет
        /// </summary>
        public bool IsHighPriority { get; set; }
        public EisenhowerMatrixCell EisenhowerMatrixCell { get; set; }
    }
}
