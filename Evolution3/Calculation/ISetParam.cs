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
        /// При нескольких вызовах возвращает наборы в одном порядке.
        /// </summary>
        /// <param name="paramsIncome">Значения входных параметров</param>
        /// <returns>Набор вариантов для одной функции и для одного набора входных параметров.</returns>
        double[,] GetSet(double[] paramsIncome);

        /// <summary>
        /// Возвращает индексы входных параметров, которые использовались для создания данного набора параметров вызова функции.
        /// </summary>
        /// <param name="indexParamSet"></param>
        /// <returns></returns>
        int[] GetIndexIncomeParams(int indexParamSet);

        /// <summary>
        /// Число различных вариантов составления набора для данной функции и числа входный параметров.
        /// </summary>
        int CountReshuffle { get; }
        /// <summary>
        /// Ссылка на выбор входных параметров.
        /// </summary>
        int[,] Reshuffle { get; }
    }
}
