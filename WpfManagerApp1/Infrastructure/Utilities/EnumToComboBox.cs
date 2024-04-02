using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfManagerApp1.Model;

namespace WpfManagerApp1.Infrastructure.Utilities
{
    internal class EnumToComboBox
    {
        private Dictionary<Type, string> typeMap = new Dictionary<Type, string>()
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
        private Dictionary<Importance, string> importanceMap = new Dictionary<Importance, string>()
        {
            { Importance.Max , "Максимальная" },
            { Importance.High , "Высокая" },
            { Importance.Medium , "Нормальная" },
            { Importance.Low , "Низкая" },
        };
        private Dictionary<CompleteStatus, string> statusMap = new Dictionary<CompleteStatus, string>()
        {
            {CompleteStatus.Active , "Активное" },
            {CompleteStatus.Frozen , "Отложено" },
            {CompleteStatus.Done , "Выполнено" },
        };

        private Dictionary<Enum, Dictionary<Type,string>> enumsMap = new Dictionary<Enum, Dictionary<Type, string>>()
        {
            {EisenhowerMatrixCell, cellMap },
        };
        public EnumToComboBox()
        {
            var a = new Array();
        }
    }
}
