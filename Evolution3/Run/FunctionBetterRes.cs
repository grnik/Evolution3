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
    /// Для функции ищем лучшее решение, для указанных входных параметров.
    /// </summary>
    public class FunctionBetterRes
    {
        public IFunction Function { get; }
        private ISetParam _setParam;
        private int _countParamsIncome;
        private FunctionSet _functionSet;
        /// <summary>
        /// Ссылка на лучший индекс указывающий на нахождение лучших параметров.
        /// </summary>
        public int BetterIndexReshuffle { get; private set; }
        public int[] BetterReshuffle { get; private set; }

        public FunctionBetterRes(ICalculation calculation, IFunction function, int countParamsIncome)
        {
            Function = function;
            _countParamsIncome = countParamsIncome;
            _setParam = Setup.GetISetParam(function, countParamsIncome);
            _functionSet = new FunctionSet(function, calculation);
        }

        /// <summary>
        /// Ищем лучшую корреляцию по входным параметрам.
        /// </summary>
        /// <param name="incomeVariants"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        public double Better(int[,] incomeVariants, int[] results)
        {
            int count = results.Length;
            if (count != incomeVariants.GetLength(0))
                throw new Exception("Не совпадает число входящих параметров и результатов");
            if (_countParamsIncome != incomeVariants.GetLength(1))
                throw new Exception("Число входных параметров не соответсвтует объявленным");
            //Результат выполнения функции на всех наборах.
            int[,] resultFuncRunSet = new int[count, _setParam.CountReshuffle];
            for (int i = 0; i < count; i++)
            {
                int[] incomeParam = FunctionSet.GetOwnSetIncomeParams(incomeVariants, i);
                int[,] incomeSetParams = _setParam.GetSet(incomeParam);
                int[] resultForSet = _functionSet.Run(incomeSetParams);
                if (_setParam.CountReshuffle != resultForSet.Length)
                    throw new Exception("Число ответов и число вариантов не совпадает");
                for (int j = 0; j < _setParam.CountReshuffle; j++)
                {
                    resultFuncRunSet[i, j] = resultForSet[j];
                }
            }

            double[] correlation = new double[_setParam.CountReshuffle];
            for (int i = 0; i < _setParam.CountReshuffle; i++)
            {
                correlation[i] = _functionSet.Correlation(results, GetArrayTo2Index(resultFuncRunSet, i));
            }

            return ChooseBetterReshuffle(correlation);
        }

        /// <summary>
        /// Выбираем лучшую корреляцию
        /// </summary>
        /// <param name="correlation"></param>
        /// <returns></returns>
        private double ChooseBetterReshuffle(double[] correlation)
        {
            BetterIndexReshuffle = 0;
            double res = 0;
            for (int i = 0; i < correlation.Length; i++)
            {
                if (Math.Abs(correlation[i]) > res)
                {
                    res = Math.Abs(correlation[i]);
                    BetterIndexReshuffle = i;
                }
            }
            BetterReshuffle = GetArrayTo1Index(_setParam.Reshuffle, BetterIndexReshuffle);

            return res;
        }

        /// <summary>
        /// Выбираем один вариант
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int[] GetArrayTo2Index(int[,] array, int index)
        {
            int count = array.GetLength(0);
            int[] res = new int[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = array[i, index];
            }

            return res;
        }

        /// <summary>
        /// Выбираем один вариант
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private int[] GetArrayTo1Index(int[,] array, int index)
        {
            int count = array.GetLength(1);
            int[] res = new int[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = array[index, i];
            }

            return res;
        }
    }
}
