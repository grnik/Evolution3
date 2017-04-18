using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using Functions;

namespace Calculation
{
    /// <summary>
    /// Выдает наборы входных параметров, для оценки приближения функций.
    /// </summary>
    public interface ISetParam
    {
        /// <summary>
        /// Выдает наборы входных параметров, для оценки приближения функций.
        /// </summary>
        /// <param name="paramsIncome">Значения входных параметров</param>
        /// <param name="function">Функция, которую будем оценивать</param>
        /// <returns></returns>
        int[,] GetSet(int[] paramsIncome, IFunction function);
    }
}
