using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    public class Calculat : ICalculation
    {
        #region Корреляция

        /// <summary>
        /// Расчет корреляции по Пирсану
        /// http://statpsy.ru/pearson/formula-pirsona/
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        double ICalculation.Correlation(List<int> x, List<int> y)
        {
            if(x.Count != y.Count)
                throw new Exception("При расчете корреляции входные последовательности не равны.");
            if(x.Count == 0)
                throw new Exception("При расчете корреляции входные последовательности пусты.");

            int x_ = AvarageValue(x);
            int y_ = AvarageValue(y);

            return SumMultAvr(x, x_, y, y_)/Math.Sqrt(SumSquareAvr(x, x_)*SumSquareAvr(y, y_));
        }

        int AvarageValue(List<int> x)
        {
            int sum = x.Sum(z => z);

            return sum;
        }

        /// <summary>
        /// Сумма произведений значений минус среднее
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x_"></param>
        /// <param name="y"></param>
        /// <param name="y_"></param>
        /// <returns></returns>
        int SumMultAvr(List<int> x, int x_, List<int> y, int y_)
        {
            int sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += (x[i] - x_)*(y[i] - y_);
            }

            return sum;
        }

        /// <summary>
        /// Сумма квадратов значения минус среднее
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x_"></param>
        /// <returns></returns>
        int SumSquareAvr(List<int> x, int x_)
        {
            int sum = x.Sum(p => (p - x_)*(p - x_));

            return sum;
        }

        #endregion
    }
}
