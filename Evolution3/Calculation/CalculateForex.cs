using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    /// <summary>
    /// Расчет корреляции дляя поиска оптимальной стратегии на форексе
    /// </summary>
    public class CalculateForex : ICalculation
    {
        public double Correlation(double[] x, double[] y)
        {
            int count = x.Length;
            if (count != y.Length)
                throw new Exception("При расчете корреляции входные последовательности не равны.");
            if (count == 0)
                throw new Exception("При расчете корреляции входные последовательности пусты.");

            double res = 0;
            for (int i = 0; i < count; i++)
            {
                res += CorrOne(x[i], y[i]);
            }

            return res / count;
        }

        private double CorrOne(double x, double y)
        {
            if (x > 10)
            {
                if (y > 10)
                {
                    return 1;
                }
                else if (y > 0)
                {
                    return 0.5;
                }
                else if (y > -10)
                {
                    return -0.5;
                }
                else
                {
                    return -1;
                }
            }
            else if (x > 0)
            {
                if (y > 10)
                {
                    return 0.5;
                }
                else if (y > 0)
                {
                    return 1;
                }
                else if (y > -10)
                {
                    return 0;
                }
                else
                {
                    return -0.5;
                }
            }
            else if (x > -10)
            {
                if (y > 10)
                {
                    return -0.5;
                }
                else if (y > 0)
                {
                    return 0;
                }
                else if (y > -10)
                {
                    return 1;
                }
                else
                {
                    return 0.5;
                }
            }
            else//x<-10
            {
                if (y > 10)
                {
                    return -1;
                }
                else if (y > 0)
                {
                    return -0.5;
                }
                else if (y > -10)
                {
                    return 0.5;
                }
                else
                {
                    return 1;
                }
            }
        }
    }
}
