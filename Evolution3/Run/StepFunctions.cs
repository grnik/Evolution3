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
        /// Результат расчета
        /// </summary>
        public RunResult RunResult { get; private set; }

        public StepFunctions(ICalculation calculation)
        {
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
        /// Массив известных решений
        /// <param name="runGuid"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public double Run(double[,] incomVariants, double[] results, Guid runGuid, int level)
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

            SetRunResult(runGuid, level);
            return BetterCorrelation;
        }

        private void SetRunResult(Guid runGuid, int level)
        {
            RunResult = new RunResult();
            RunResult.Id = Guid.NewGuid();
            RunResult.RunId = runGuid;
            FunctionBetterRes functionBetter = FunctionBetter[BetterCorrelationIndex];
            RunResult.Function = functionBetter.Function.Name;
            RunResult.RunTime = DateTime.Now;
            RunResult.Result = BetterCorrelation;
            RunResult.Level = level;
            //result.OrderCondition = orderCondition;
            //if (!indexChange.HasValue)
            //    throw new NotImplementedException("При малой корреляции с входным параметром - сделать не замену. а добавление параметра.");
            //RunResult.IndexOut = indexChange.Value;
            RunResult.StandardDeviation = functionBetter.StandardDeviation;
            RunResult.Parameters = new List<RunResultParam>();
            for (int i = 0; i < functionBetter.Function.ParamCount; i++)
            {
                RunResultParam param = new RunResultParam();
                param.Id = Guid.NewGuid();
                param.RunResult = RunResult;
                param.OrderParam = i;
                param.IndexParam = functionBetter.BetterReshuffle[i];

                RunResult.Parameters.Add(param);
            }
        }

        /// <summary>
        /// Сохраняем результат выполнения.
        /// </summary>
        /// <param name="indexChange">Номер входного параметра для замены. Не определен - новый параметр</param>
        /// <param name="orderCondition"></param>
        /// <returns>RunResult</returns>
        public RunResult SaveResult(int? indexChange, int? orderCondition)
        {
            using (EvoluationContext context = new EvoluationContext())
            {
                return SaveResult(context, indexChange, orderCondition);
            }
        }

        internal RunResult SaveResult(EvoluationContext context, int? indexChange, int? orderCondition)
        {
            RunResult.OrderCondition = orderCondition;
            if (!indexChange.HasValue)
                throw new NotImplementedException("При малой корреляции с входным параметром - сделать не замену. а добавление параметра.");
            RunResult.IndexOut = indexChange.Value;
            FunctionBetterRes functionBetter = FunctionBetter[BetterCorrelationIndex];
            context.RunResults.Add(RunResult);
            //for (int i = 0; i < functionBetter.Function.ParamCount; i++)
            //{
            //    RunResultParam param = new RunResultParam();
            //    param.Id = Guid.NewGuid();
            //    param.RunResult = RunResult;
            //    param.OrderParam = i;
            //    param.IndexParam = functionBetter.BetterReshuffle[i];
            //    context.RunResultParams.Add(param);
            //}
            foreach (RunResultParam param in RunResult.Parameters)
            {
                context.RunResultParams.Add(param);
            }

            context.SaveChanges();

            return RunResult;
        }

    }
}
