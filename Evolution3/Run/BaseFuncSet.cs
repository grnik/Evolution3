using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    public class BaseFuncSet
    {

        /// <summary>
        /// Возвращает результат сравнения.
        /// 1- d1 > d2
        /// -1 - d1 < d2
        /// 0 - d1 = d2
        /// Nan - один из операторов NaN
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static double CompareDouble(double d1, double d2)
        {
            return Functions.CompareDouble.Compare(d1, d2);
            ////Точность сравнения.
            //const double precision = 1E-7;

            //if (Double.IsNaN(d1) || Double.IsNaN(d2))
            //    return Double.NaN;
            //if (Math.Abs(d1 - d2) < precision)
            //    return 0;

            //return d1 > d2 ? 1 : -1;
        }

        /// <summary>
        /// Копируем конкретный набор входных параметров из списка вариантов.
        /// </summary>
        /// <param name="setParams"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static double[] GetOwnSetIncomeParams(double[,] setParams, int index)
        {
            int count = setParams.GetLength(1);
            double[] res = new double[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = setParams[index, i];
            }

            return res;
        }
    }
}
