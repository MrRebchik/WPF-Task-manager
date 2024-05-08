using System;
using System.Collections.Generic;
using System.Linq;
using WpfManagerApp1.Model;
using WpfManagerApp1.Services;

namespace WpfManagerApp1.Infrastructure.Utilities
{
    internal static class EnumToComboBox
    {
        public static Dictionary<Type, string> typeMap = new Dictionary<Type, string>()
        {
            {typeof(UniqueWork), "Одноразовое задание"},
            {typeof(RegularWork), "Повторяемое задание"},
        };
        public static Dictionary<ConsoleColor, string> cc;
        public static Dictionary<EisenhowerMatrixCell, string> cellMap = new Dictionary<EisenhowerMatrixCell, string>()
        {
            {EisenhowerMatrixCell.ImportantImmediately, "Важное срочное" },
            {EisenhowerMatrixCell.UnimportantImmediately , "Не важное срочное" },
            {EisenhowerMatrixCell.ImportantUnimmediately , "Важное не срочное" },
            {EisenhowerMatrixCell.UnimportantUnimmediately , "Не важное не срочное" },
        };
        public static Dictionary<Importance, string> importanceMap = new Dictionary<Importance, string>()
        {
            { Importance.Max , "Максимальная" },
            { Importance.High , "Высокая" },
            { Importance.Medium , "Нормальная" },
            { Importance.Low , "Низкая" },
        };
        public static Dictionary<CompleteStatus, string> statusMap = new Dictionary<CompleteStatus, string>()
        {
            {CompleteStatus.Active , "Активное" },
            {CompleteStatus.Frozen , "Отложено" },
            {CompleteStatus.Done , "Выполнено" },
        };
        public static Dictionary<SortFilters, string> worksFiltersMap = new Dictionary<SortFilters, string>()
        {
            {SortFilters.ByImmediacy ,                     "По срочности" },
            {SortFilters.ByImportance,                     "По важности" },
            {SortFilters.ByImmediacyDescending,            "Сначала не срочные" },
            {SortFilters.ByImportanceDescending,           "Сначала не важные" }, 
            {SortFilters.ByHabits ,                        "Повторяемые" },
            {SortFilters.ByCreationDate,                   "По умолчанию" },
            //{SortFilters.ByImmediacyWithHabits,            "Сначала срочные привычки" },
            //{SortFilters.ByImmediacyWithHabitsDescending,  "Сначала не срочные привычки" },
        };

        public static List<string> EnumToList<T>(Dictionary<T, string> map)
        {
            return map.Values.ToList();
        }

        public static T GetEnumValueFromComboBox<T>(Dictionary<T, string> map, string value)
        {
            return map.Where(x => x.Value == value).Select(x => x.Key).FirstOrDefault();
    }
}
}
