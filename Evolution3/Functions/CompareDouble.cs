using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class CompareDouble
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
        public static double Compare(double d1, double d2)
        {
            //Точность сравнения.
            const double precision = 1E-7;

            if (Double.IsNaN(d1) || Double.IsNaN(d2))
                return Double.NaN;
            if (Math.Abs(d1 - d2) < precision)
                return 0;

            return d1 > d2 ? 1 : -1;
        }
    }
}
