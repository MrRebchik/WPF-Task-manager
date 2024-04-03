using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using WpfManagerApp1.Model;

namespace WpfManagerApp1.Infrastructure.Utilities
{
    internal class EnumToComboBox
    {
        static Enum b = ConsoleColor.Red;
        Type eisMatrixCell = Enum.GetUnderlyingType(b);

        private static Dictionary<Type, string> typeMap = new Dictionary<Type, string>()
        {
            {typeof(UniqueWork), "Одноразовое задание"},
            {typeof(RegularWork), "Повторяемое задание"},
        };
        private static Dictionary<EisenhowerMatrixCell, string> cellMap = new Dictionary<EisenhowerMatrixCell, string>()
        {
            {EisenhowerMatrixCell.ImportantImmediately, "Важное срочное" },
            {EisenhowerMatrixCell.UnimportantImmediately , "Не важное срочное" },
            {EisenhowerMatrixCell.ImportantUnimmediately , "Важное не срочное" },
            {EisenhowerMatrixCell.UnimportantUnimmediately , "Не важное не срочное" },
        };
        private static Dictionary<Importance, string> importanceMap = new Dictionary<Importance, string>()
        {
            { Importance.Max , "Максимальная" },
            { Importance.High , "Высокая" },
            { Importance.Medium , "Нормальная" },
            { Importance.Low , "Низкая" },
        };
        private static Dictionary<CompleteStatus, string> statusMap = new Dictionary<CompleteStatus, string>()
        {
            {CompleteStatus.Active , "Активное" },
            {CompleteStatus.Frozen , "Отложено" },
            {CompleteStatus.Done , "Выполнено" },
        };

        List<Dictionary<Type, string>> a = new List<Dictionary<Type, string>>()
        {
            typeMap ,
        };

        
        List<string> GetList<T>()
        {
            var targetMap = new Dictionary<T, string>();

            return EnumToList(targetMap);
        }
        private List<string> EnumToList<T>(Dictionary<T, string> map)
        {
            return map.Values.ToList();
        }
        public EnumToComboBox()
        {
            Type a = typeof(EisenhowerMatrixCell);
        }
    }
}
