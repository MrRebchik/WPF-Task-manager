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
    public delegate void WorkPropertyChangedHandler(Work item);
    public abstract class Work
    {
        private string name = "Укажите название";
        private string description = "Добавить описание";
        private CompleteStatus completeness;
        private Importance importance;
        private int durationInMinutes;
        private DateTime creationDate;
        private bool isHighPriority;
        private EisenhowerMatrixCell eisenhowerMatrixCell;

        public event WorkPropertyChangedHandler WorkPropertyChanged;
        public Work(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
        public string Name
        { 
            get => name; 
            set
            {
                name = value;
                OnWorkPropertyChanged();
            }
        }
        public string Description 
        { 
            get => description;
            set
            {
                description = value;
                OnWorkPropertyChanged();
            }
        }
        public CompleteStatus Completeness 
        { 
            get => completeness;
            set
            {
                completeness = value;
                OnWorkPropertyChanged();
            }
        }
        public Importance Importance 
        { 
            get => importance; 
            set 
            {
                importance = value;
                OnWorkPropertyChanged();
            } 
        }
        /// <summary>
        /// Примерное время затрачиваемое на задание (мин.)
        /// </summary>
        public int DurationInMinutes 
        { 
            get => durationInMinutes; 
            set 
            {
                durationInMinutes = value;
                OnWorkPropertyChanged();
            } 
        }
        public DateTime CreationDate 
        { 
            get => creationDate; 
            set 
            {
                creationDate = value;
                OnWorkPropertyChanged();
            } 
        }
        /// <summary>
        /// Устанавливается true для дел которые получают особый приоритет
        /// </summary>
        public bool IsHighPriority 
        { 
            get => isHighPriority; 
            set 
            {
                isHighPriority = value;
                OnWorkPropertyChanged();
            } 
        }
        public EisenhowerMatrixCell EisenhowerMatrixCell 
        { 
            get => eisenhowerMatrixCell; 
            set 
            {
                eisenhowerMatrixCell = value;
                OnWorkPropertyChanged();
            } 
        }

        public void OnWorkPropertyChanged()
        {
            WorkPropertyChanged?.Invoke((Work)this);
        }
    }
}
