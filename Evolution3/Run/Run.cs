using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using DataDB;
using Functions;

namespace Run
{
    /// <summary>
    /// Запускаем подбор.
    /// </summary>
    public class Run
    {
        /// <summary>
        /// Первое измерение - номер варианта
        /// Второе измерение - индекс 
        /// </summary>
        private double[,] _incomeVariants;
        private double[] _results;
        private int _countParamIndex;
        private DataDB.Setup _setup;
        ICalculation _calculation;

        //Считываем данные из БД
        public Run()
        {
            _calculation = Setup.GetICalculation();
            using (EvoluationContext context = new EvoluationContext())
            {
                _setup = context.Setups.First();
                _countParamIndex = _setup.CountParamIndex;

                _incomeVariants = new double[context.InputDatas.Count() / _countParamIndex, _countParamIndex];
                foreach (InputData inputData in context.InputDatas)
                {
                    _incomeVariants[inputData.IncomeIndexId, inputData.ParamIndexId] = inputData.Value;
                }

                _results = new double[context.Results.Count()];
                foreach (Result result in context.Results)
                {
                    _results[result.IncomeIndexId] = result.Value;
                }
            }
        }

        /// <summary>
        /// Запускаем с переданными данными
        /// </summary>
        /// <param name="incomeVariants"></param>
        /// <param name="results"></param>
        public Run(double[,] incomeVariants, double[] results)
        {
            _countParamIndex = incomeVariants.GetLength(1);
            _incomeVariants = incomeVariants;
            _results = results;
        }

        /// <summary>
        /// Запускаем поиск решения
        /// </summary>
        public double Search()
        {
            Guid idRun = Guid.NewGuid();
            Step0 step0 = new Step0(_setup, _calculation);
            int level = 0;
            do
            {
                double bettCorr = step0.Run(_incomeVariants, _results, level);

                //Изменяем входные параметры на новые.
                int indexForChange = GetIndexChange(_incomeVariants, step0.FunctionBetter[step0.BetterCorrelationIndex].BetterResult);
                ChangeIncomeVariants(ref _incomeVariants, step0.FunctionBetter[step0.BetterCorrelationIndex].BetterResult, indexForChange);

                SaveResult(idRun, step0, bettCorr, level, indexForChange);

                if ((level >= _setup.MaxLevel) || (bettCorr > _setup.TargetCorrelation))
                {
                    return bettCorr;// = Run(_incomeVariants, results, level + 1);
                }
                level++;
            } while (true);
        }

        /// <summary>
        /// Сохраняем результат выполнения.
        /// </summary>
        /// <param name="runGuid">Идентификатор данного выполнения.</param>
        /// <param name="step"></param>
        /// <param name="bettCorr"></param>
        /// <param name="level"></param>
        private void SaveResult(Guid runGuid, Step0 step, double bettCorr, int level, int indexForChange)
        {
            using (EvoluationContext context = new EvoluationContext())
            {
                RunResult result = new RunResult();
                result.Id = Guid.NewGuid();
                result.RunId = runGuid;
                FunctionBetterRes functionBetter = step.FunctionBetter[step.BetterCorrelationIndex];
                result.Function = functionBetter.Function.Name;
                result.RunTime = DateTime.Now;
                result.Result = bettCorr;
                result.Level = level;
                result.IndexOut = indexForChange;
                result.StandardDeviation = functionBetter.StandardDeviation;
                context.RunResults.Add(result);
                for (int i = 0; i < functionBetter.Function.ParamCount; i++)
                {
                    RunResultParam param = new RunResultParam();
                    param.Id = Guid.NewGuid();
                    param.RunResult = result;
                    param.OrderParam = i;
                    param.IndexParam = functionBetter.BetterReshuffle[i];
                    context.RunResultParams.Add(param);
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Определяем какой входной параметр заменить. 
        /// Индекс входного параметра с наибольшей корреляцией с результатом.
        /// </summary>
        /// <param name="incomVariants"></param>
        /// <param name="betterResult"></param>
        /// <returns>Индекс входного параметра для замены.</returns>
        private int GetIndexChange(double[,] incomVariants, double[] betterResult)
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
        private static void ChangeIncomeVariants(ref double[,] incomVariants, double[] forChangeArray, int index)
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
