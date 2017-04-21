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

        public Step0(DataDB.Setup setup)
        {
            _setup = setup;
            _calculation = Setup.GetICalculation();
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
        /// <returns></returns>
        public double Run(int[,] incomVariants, int[] results, int level = 0)
        {
            int count = _functions.Count;
            int countIncomeParams = incomVariants.GetLength(1);
            double[] betterCorrelations = new double[count];
            FunctionBetter = new List<FunctionBetterRes>(count);
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

            //Изменяем входные параметры на новые.
            int indexForChange = GetIndexChange(incomVariants, FunctionBetter[BetterCorrelationIndex].BetterResult);
            ChangeIncomeVariants(ref incomVariants, FunctionBetter[BetterCorrelationIndex].BetterResult, indexForChange);

            if (level <= _setup.MaxLevel)
            {
                if (bettCorr < _setup.TargetCorrelation)
                {
                    bettCorr = Run(incomVariants, results, level + 1);
                }
            }

            return bettCorr;
        }

        /// <summary>
        /// Определяем какой входной параметр заменить. 
        /// Индекс входного параметра с наибольшей корреляцией с результатом.
        /// </summary>
        /// <param name="incomVariants"></param>
        /// <param name="betterResult"></param>
        /// <returns>Индекс входного параметра для замены.</returns>
        private int GetIndexChange(int[,] incomVariants, int[] betterResult)
        {
            int count = incomVariants.GetLength(1);
            //double[] correlations = new double[count];
            double bestCorr = 0;
            int bestIndex = 0;

            for (int i = 0; i < count; i++)
            {
                double correlation = Math.Abs(_calculation.Correlation(ArrayCopy.GetArrayTo2Index(incomVariants, 1), betterResult));
                if (correlation > bestCorr)
                {
                    bestIndex = i;
                    bestCorr = correlation;
                }
            }

            return bestIndex;
        }


        /// <summary>
        /// В исходных параметрах меняем данные на результат выполнения лучшей функции.
        /// </summary>
        /// <param name="incomVariants"></param>
        /// <param name="forChangeArray"></param>
        /// <param name="index"></param>
        private void ChangeIncomeVariants(ref int[,] incomVariants, int[] forChangeArray, int index)
        {
            int count = incomVariants.GetLength(0);
            if (count != forChangeArray.Length)
                throw new Exception("Размерность входных параметров и результата на замену - не совпадает");
            for (int i = 0; i < count; i++)
            {
                incomVariants[i, index] = forChangeArray[i];
            }
        }
    }
}
