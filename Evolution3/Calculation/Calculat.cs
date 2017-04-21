using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    //TODO: Рассчмотреть регрисионный анализ.http://medstatistic.ru/theory/pirson.html
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
        public double Correlation(int[] x, int[] y)
        {
            checked
            {
                int count = x.Length;
                if (count != y.Length)
                    throw new Exception("При расчете корреляции входные последовательности не равны.");
                if (count == 0)
                    throw new Exception("При расчете корреляции входные последовательности пусты.");


                //http://cito-web.yspu.org/link1/metod/met125/node35.html
                double sumX = SumX(x);
                double sumY = SumX(y);
                double corr = (count * SumMultiply(x, y) - (sumX * sumY))
                    / Math.Sqrt((count * SumSquare(x) - Math.Pow(sumX, 2)) * (count * SumSquare(y) - Math.Pow(sumY, 2)));
                return corr;
            }
        }

        /// <summary>
        /// Сумма массива
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        double SumX(int[] x)
        {
            return x.Sum(p => (double)p);
        }

        /// <summary>
        /// Сумма произведений массивов
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        double SumMultiply(int[] x, int[] y)
        {
            int count = x.Length;
            if (count != y.Length)
                throw new Exception("Величины массивов не совпадают");

            double sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += x[i] * y[i];
            }

            return sum;
        }

        double SumSquare(int[] x)
        {
            return x.Sum(p => Math.Pow(p, 2));
            //double sum = 0;
            //for (int i = 0; i < x.Length; i++)
            //{
            //    sum += Math.Pow(x[i], 2);
            //}
            //return sum;
        }

        #region Старый вариант корреляции

        /// <summary>
        /// Расчет корреляции по Пирсану
        /// http://statpsy.ru/pearson/formula-pirsona/
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        double CorrelationOld(int[] x, int[] y)
        {
            checked
            {
                if (x.Length != y.Length)
                    throw new Exception("При расчете корреляции входные последовательности не равны.");
                if (x.Length == 0)
                    throw new Exception("При расчете корреляции входные последовательности пусты.");

                int x_ = AvarageValue(x);
                int y_ = AvarageValue(y);

                double deletel = Math.Sqrt(SumSquareAvr(x, x_) * SumSquareAvr(y, y_));
                double chastnoe = SumMultAvr(x, x_, y, y_);
                double corr = chastnoe / deletel;
                return corr;
            }
        }

        int AvarageValue(int[] x)
        {
            int sum = x.Sum(z => z);

            return sum / x.Length;
        }

        /// <summary>
        /// Сумма произведений значений минус среднее
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x_"></param>
        /// <param name="y"></param>
        /// <param name="y_"></param>
        /// <returns></returns>
        int SumMultAvr(int[] x, int x_, int[] y, int y_)
        {
            int sum = 0;
            for (int i = 0; i < x.Length; i++)
            {
                sum += (x[i] - x_) * (y[i] - y_);
            }

            return sum;
        }

        /// <summary>
        /// Сумма квадратов значения минус среднее
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x_"></param>
        /// <returns></returns>
        int SumSquareAvr(int[] x, int x_)
        {
            int sum = x.Sum(p => (p - x_) * (p - x_));

            return sum;
        }

        #endregion

        #endregion
    }
}
