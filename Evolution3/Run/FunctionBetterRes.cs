﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using Functions;
using Ninject;
using Ninject.Parameters;

namespace Run
{
    /// <summary>
    /// Для функции ищем лучшее решение, для указанных входных параметров.
    /// </summary>
    public class FunctionBetterRes
    {
        private static IKernel AppKernel = new StandardKernel(new NinjectConfig());

        public IFunction Function { get; }
        private ISetParam _setParam;
        private int _countParamsIncome;
        private FunctionSet _functionSet;
        /// <summary>
        /// Ссылка на лучший индекс указывающий на нахождение лучших параметров.
        /// </summary>
        public int BetterIndexReshuffle { get; private set; }
        public int[] BetterReshuffle { get; private set; }
        /// <summary>
        /// Лучший результат выполения.
        /// </summary>
        public double[] BetterResult { get; private set; }
        /// <summary>
        /// Все результаты выполнения.
        /// Первый индекс - Набор параметров
        /// Второй индекс - Число вариантов параметров. BetterIndexReshuffle - показывает какой брать для лучшего решения.
        /// </summary>
        public double[,] AllResultsRun { get; private set; }
        /// <summary>
        /// Корреляция для всех вариантов параметров. BetterIndexReshuffle - показывает какой брать для лучшего решения.
        /// </summary>

        public double[] Correlation { get; private set; }

        public double StandardDeviation { get; private set; }

        public FunctionBetterRes(ICalculation calculation, IFunction function, int countParamsIncome)
        {
            Function = function;
            _countParamsIncome = countParamsIncome;
            //_setParam = Setup.GetISetParam(function, countParamsIncome);
            _setParam = AppKernel.Get<ISetParam>(new[]
            {
                new ConstructorArgument("function", function),
                new ConstructorArgument("countParamsIncome", countParamsIncome)
            });
            _functionSet = new FunctionSet(function, calculation);
        }

        /// <summary>
        /// Ищем лучшую корреляцию по входным параметрам.
        /// </summary>
        /// <param name="incomeVariants">
        /// Первое измерение - номер варианта
        /// Второе измерение - индекс 
        /// </param>
        /// <param name="results"></param>
        /// <returns></returns>
        public double Better(double[,] incomeVariants, double[] results)
        {
            int count = results.Length;
            if (count != incomeVariants.GetLength(0))
                throw new Exception("Не совпадает число входящих параметров и результатов");
            if (_countParamsIncome != incomeVariants.GetLength(1))
                throw new Exception("Число входных параметров не соответсвтует объявленным");
            //Результат выполнения функции на всех наборах.
            AllResultsRun = new double[count, _setParam.CountReshuffle];
            for (int i = 0; i < count; i++)
            {
                double[] incomeParam = BaseFuncSet.GetOwnSetIncomeParams(incomeVariants, i);
                double[,] incomeSetParams = _setParam.GetSet(incomeParam);
                double[] resultForSet = _functionSet.Run(incomeSetParams);
                if (_setParam.CountReshuffle != resultForSet.Length)
                    throw new Exception("Число ответов и число вариантов не совпадает");
                for (int j = 0; j < _setParam.CountReshuffle; j++)
                {
                    AllResultsRun[i, j] = resultForSet[j];
                }
            }

            Correlation = new double[_setParam.CountReshuffle];
            for (int i = 0; i < _setParam.CountReshuffle; i++)
            {
                Correlation[i] = _functionSet.Correlation(results, ArrayCopy.GetArrayTo2Index(AllResultsRun, i));
            }

            return ChooseBetterReshuffle(results);
        }

        /// <summary>
        /// Выбираем лучшую корреляцию
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        private double ChooseBetterReshuffle(double[] results)
        {
            BetterIndexReshuffle = 0;
            double res = 0;
            StandardDeviation = Double.MaxValue;
            for (int i = 0; i < Correlation.Length; i++)
            {
                //Если корреляция совпадает, то выбираем решение с наименьшим среднеквадратичным отклонением.
                if (BaseFuncSet.CompareDouble(Math.Abs(Correlation[i]), res) == 0)
                {
                    double[] resultsRun = ArrayCopy.GetArrayTo2Index(AllResultsRun, i);
                    double stDev = GetStandardDeviation(results, resultsRun);
                    if (BaseFuncSet.CompareDouble(stDev, StandardDeviation) < 0)
                    {
                        res = Math.Abs(Correlation[i]);
                        BetterIndexReshuffle = i;
                        BetterResult = resultsRun;
                        StandardDeviation = stDev;
                    }
                }
                //Выбираем решение с меньшей корреляцией
                else if (Math.Abs(Correlation[i]) > res)
                {
                    res = Math.Abs(Correlation[i]);
                    BetterIndexReshuffle = i;
                    BetterResult = ArrayCopy.GetArrayTo2Index(AllResultsRun, BetterIndexReshuffle);
                    StandardDeviation = GetStandardDeviation(results, BetterResult);
                }
            }
            BetterReshuffle = ArrayCopy.GetArrayTo1Index(_setParam.Reshuffle, BetterIndexReshuffle);

            return res;
        }

        /// <summary>
        /// Среднеквадротичное отклонение
        /// </summary>
        /// <returns></returns>
        private double GetStandardDeviation(double[] array1, double[] array2)
        {
            int count = array1.Length;
            if (count != array2.Length)
            {
                throw new Exception("Нельзя посчитать среднеквадротичное отклонение на массивах разной длины.");
            }
            double res = 0;
            for (int i = 0; i < count; i++)
            {
                res += Math.Pow(array1[1] - array2[i], 2);
            }

            return res;
        }
    }
}
