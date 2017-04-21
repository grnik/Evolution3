using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDB
{
    public class Setup
    {
        public int Id { get; set; }
        /// <summary>
        /// Сколько входных параметров в каждом запросе.
        /// </summary>
        public int CountParamIndex { get; set; }
        /// <summary>
        /// Максимальный уровень вложенности, для ограничения числа операций
        /// </summary>
        public int MaxLevel { get; set; }

        /// <summary>
        /// Какой корреляции хотим добиться.
        /// </summary>
        public double TargetCorrelation { get; set; }
    }
}
