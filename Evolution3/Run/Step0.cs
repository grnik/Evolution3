using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using Functions;

namespace Run
{
    /// <summary>
    /// Первый шаг в выполнении
    /// </summary>
    internal class Step0
    {
        private readonly List<IFunction> _functions;
        public List<FunctionBetterRes> FunctionBetter { get; private set; }
        public int BetterCorrelationIndex { get; private set; }

        public Step0()
        {
            _functions = FFactory.AllFunction();
        }

        public double Run(int[,] incomVariants, int[] results)
        {
            int count = _functions.Count;
            int countIncomeParams = incomVariants.GetLength(1);
            double[] betterCorrelations = new double[count];
            ICalculation calculation = Setup.GetICalculation();
            FunctionBetter = new List<FunctionBetterRes>(count);
            for (int i = 0; i < count; i++)
            {
                FunctionBetter.Add(new FunctionBetterRes(calculation, _functions[i], countIncomeParams));
                betterCorrelations[i] = FunctionBetter[i].Better(incomVariants, results);
            }

            //Выбираем лучшее решение
            double bettCorr = betterCorrelations[0];
            BetterCorrelationIndex = 0;
            for (int i = 1; i < count; i++)
            {
                if (bettCorr < betterCorrelations[i])
                {
                    bettCorr = betterCorrelations[i];
                    BetterCorrelationIndex = i;
                }
            }

            return bettCorr;
        }
    }
}
