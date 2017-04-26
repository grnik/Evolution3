using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
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
        private ICalculation _calculation;
        private readonly List<IFunction> _functions;
        public List<FunctionBetterRes> FunctionBetter { get; private set; }
        public int BetterCorrelationIndex { get; private set; }
        private DataDB.Setup _setup;

        public Step0(DataDB.Setup setup, ICalculation calculation)
        {
            _setup = setup;
            _calculation = calculation;
            _functions = FFactory.AllFunction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="incomVariants">
        /// Первое измерение - номер варианта
        /// Второе измерение - индекс 
        /// </param>
        /// <param name="results"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public double Run(double[,] incomVariants, double[] results, int level = 0)
        {
            int count = _functions.Count;
            int countIncomeParams = incomVariants.GetLength(1);
            double[] betterCorrelations = new double[count];
            FunctionBetter = new List<FunctionBetterRes>();
            for (int i = 0; i < count; i++)
            {
                FunctionBetter.Add(new FunctionBetterRes(_calculation, _functions[i], countIncomeParams));
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
