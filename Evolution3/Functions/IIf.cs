using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    /// <summary>
    /// Условие.
    /// По параметрам возвращает число, которое позволяет разделять входную последовательность на подпоследовательности (с одинаковым номером).
    /// Решение только для отдельных элементов, для остальных значение по умолчанию.
    /// </summary>
    public interface IIf : IBaseFunc
    {
        /// <summary>
        /// Результат выполнения условия
        /// </summary>
        /// <param name="paramInput"></param>
        /// <returns></returns>
        int Run(params double[] paramInput);
    }
}
