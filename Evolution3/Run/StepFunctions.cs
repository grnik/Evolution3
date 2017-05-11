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
    /// Для заданных входных и выходных параметров находим лучшую функцию и ее параметры.
    /// Без учета условий.
    /// </summary>
    public class StepFunctions
    {
        private readonly ICalculation _calculation;
        private readonly List<IFunction> _functions;
        /// <summary>
        /// Лучшие решения, построенные для каждой функции.
        /// </summary>
        public List<FunctionBetterRes> FunctionBetter { get; private set; }
        /// <summary>
        /// Индекс лучшего решения. Среди всех функций.
        /// </summary>
        public int BetterCorrelationIndex { get; private set; }
        public double BetterCorrelation { get; private set; }

        /// <summary>
        /// Какой входной параметр менять результатом выполнения функции.
        /// Определяем самый близкий (по корреляции) параметр к результату и меняем его.
        /// </summary>
        public int? IndexChange { get; private set; }

        public StepFunctions(ICalculation calculation)
        {
            _calculation = calculation;
            _functions = FFactory.AllFunction();
            IndexChange = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="incomVariants">
        /// Первое измерение - номер варианта
        /// Второе измерение - индекс 
        /// </param>
        /// <param name="results"></param>
        /// Массив известных решений
        /// <returns></returns>
        public double Run(double[,] incomVariants, double[] results)
        {
            IndexChange = null;
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
            BetterCorrelation = betterCorrelations[0];
            BetterCorrelationIndex = 0;
            for (int i = 1; i < count; i++)
            {
                if (BetterCorrelation < betterCorrelations[i])
                {
                    BetterCorrelation = betterCorrelations[i];
                    BetterCorrelationIndex = i;
                }
            }

            GetIndexChange(incomVariants);

            return BetterCorrelation;
        }

        /// <summary>
        /// Определяем какой входной параметр заменить. 
        /// Индекс входного параметра с наибольшей корреляцией с результатом.
        /// </summary>
        /// <param name="incomVariants"></param>
        /// <param name="betterResult"></param>
        /// <returns>Индекс входного параметра для замены.</returns>
        private void GetIndexChange(double[,] incomVariants)
        {
            double[] betterResult = FunctionBetter[BetterCorrelationIndex].BetterResult;

            int count = incomVariants.GetLength(1);
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

            IndexChange = bestIndex;
        }

        /// <summary>
        /// Сохраняем результат выполнения.
        /// </summary>
        /// <param name="runGuid">Идентификатор данного выполнения.</param>
        /// <param name="level"></param>
        /// <param name="orderCondition"></param>
        /// <returns>RunResult</returns>
        public RunResult SaveResult(Guid runGuid, int level, int? orderCondition)
        {
            using (EvoluationContext context = new EvoluationContext())
            {
                return SaveResult(context, runGuid, level, orderCondition);
            }
        }
        internal RunResult SaveResult(EvoluationContext context, Guid runGuid, int level, int? orderCondition)
        {
            RunResult result = new RunResult();
            result.Id = Guid.NewGuid();
            result.RunId = runGuid;
            FunctionBetterRes functionBetter = FunctionBetter[BetterCorrelationIndex];
            result.Function = functionBetter.Function.Name;
            result.RunTime = DateTime.Now;
            result.Result = BetterCorrelation;
            result.Level = level;
            result.OrderCondition = orderCondition;
            if (!IndexChange.HasValue)
                throw new Exception("Не определен индекс выходного параметра для функции.");
            result.IndexOut = IndexChange.Value;
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

            return result;
        }

    }
}
