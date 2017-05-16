using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using DataDB;
using Functions;

namespace Run
{
    /// <summary>
    /// Перебираем возможности выполнения с условием.
    /// По условию создаем отдельные группы входных параметров. На каждой группе считаем корреляцию. Если корреляция больше чем по умолчанию, то 
    /// выполняем с данным условием.
    /// </summary>
    public class StepCondition
    {
        private readonly ICalculation _calculation;
        private readonly List<IIf> _conditions;

        /// <summary>
        /// Для каждого набора входных параметров (получившихся из условий) ищем лучшее решение.
        /// </summary>
        public StepFunctions[] StepFunctions { get; private set; }

        /// <summary>
        /// Список решений для условий
        /// </summary>
        public ConditionAllDecisions Decisions;

        /// <summary>
        /// Корреляция в зависимости от решения. Индекс корреляции и решения совпадает.
        /// </summary>
        public double[] Correlation { get; private set; }
        public ConditionBetterRes[] ConditionBetterRes { get; private set; }

        public StepCondition(ICalculation calculation)
        {
            _calculation = calculation;
            _conditions = IfFactory.AllIf();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="incomVariants">
        /// Первое измерение - номер варианта
        /// Второе измерение - индекс в конкретном номера варианта.
        /// </param>
        /// <param name="results"></param>
        /// Массив известных решений
        /// <returns></returns>
        public void Run(double[,] incomVariants, double[] results)
        {
            int count = _conditions.Count;
            int countIncomeParams = incomVariants.GetLength(1);
            //double[] betterCorrelations = new double[count];
            //Получаем все возможные варианты набора входных параметров.
            Decisions = new ConditionAllDecisions(countIncomeParams);
            ConditionBetterRes = new ConditionBetterRes[count];
            for (int i = 0; i < count; i++)
            {
                ConditionBetterRes[i] = new ConditionBetterRes(_conditions[i], countIncomeParams);
                ConditionBetterRes[i].SetDecision(incomVariants, ref Decisions);
            }

            //Для каждого набора входных параметров ищем лучшее решение.
            CalculatCorrelation(incomVariants, results);

            //Выбираем лучшее решение
            //double bettCorr = betterCorrelations[0];
            //BetterCorrelationIndex = 0;
            //for (int i = 1; i < count; i++)
            //{
            //    if (bettCorr < betterCorrelations[i])
            //    {
            //        bettCorr = betterCorrelations[i];
            //        BetterCorrelationIndex = i;
            //    }
            //}

            //return bettCorr;
        }

        /// <summary>
        /// Вычисляем корреляцию, для всех вариантов решений, полученных перебором условий.
        /// </summary>
        /// <param name="incomVariants"></param>
        /// <param name="results"></param>
        private void CalculatCorrelation(double[,] incomVariants, double[] results)
        {
            int countDecision = Decisions.Decisions.Count;
            StepFunctions = new StepFunctions[countDecision];
            Correlation = new double[countDecision];
            for (int i = 0; i < countDecision; i++)
            {
                double[,] partIncomeVariants = GetPartIncomeVariants(Decisions.Decisions[i].Decision, incomVariants);
                double[] partResults = GetPartResults(Decisions.Decisions[i].Decision, results);
                StepFunctions[i] = new StepFunctions(_calculation);
                Correlation[i] = StepFunctions[i].Run(partIncomeVariants, partResults);
            }
        }

        /// <summary>
        /// Возвращаем часть решений по указанной матрице.
        /// </summary>
        /// <param name="decision"></param>
        /// <param name="results"></param>
        /// <returns></returns>
        private double[] GetPartResults(bool[] decision, double[] results)
        {
            int countIncomVariant = decision.Length;
            if (countIncomVariant != results.GetLength(0))
                throw new Exception("Количество решений и число известных ответов не совпадает.");
            int countNewVariant = decision.Count(d => d);
            double[] res = new double[countNewVariant];
            int k = 0;
            for (int i = 0; i < countIncomVariant; i++)
            {
                if (decision[i])
                {
                    res[k] = results[i];
                    k++;
                }
            }

            return res;
        }

        /// <summary>
        /// Возвращаем часть входных параметров по указанной матрице.
        /// </summary>
        /// <param name="decision"></param>
        /// <param name="incomVariants"></param>
        /// <returns></returns>
        private double[,] GetPartIncomeVariants(bool[] decision, double[,] incomVariants)
        {
            int countIncomVariant = decision.Length;
            if (countIncomVariant != incomVariants.GetLength(0))
                throw new Exception("Количество решений и число входных вариантов не совпадает.");
            int countNewVariant = decision.Count(d => d);
            int countParam = incomVariants.GetLength(1);
            double[,] res = new double[countNewVariant, countParam];
            int k = 0;
            for (int i = 0; i < countIncomVariant; i++)
            {
                if (decision[i])
                {
                    for (int j = 0; j < countParam; j++)
                    {
                        res[k, j] = incomVariants[i, j];
                    }
                    k++;
                }
            }

            return res;
        }


        /// <summary>
        /// Сохраняем результат выполнения.
        /// </summary>
        /// <param name="runGuid">Идентификатор данного выполнения.</param>
        /// <param name="level"></param>
        public void SaveResult(Guid runGuid, int level)
        {
            int countDecision = Decisions.Decisions.Count;
            if (countDecision != StepFunctions.Length)
                throw new Exception("Число решений в AllDecision и StepRun должно совпадать");
            using (EvoluationContext context = new EvoluationContext())
            {
                for (int i = 0; i < countDecision; i++)
                {
                    RunResult runResult = StepFunctions[i].SaveResult(context, runGuid, level, i);

                    foreach (ConditionVariantResult conditionVariantResult in Decisions.Decisions[i].ConditionVariantResults)
                    {
                        RunCondition runCondition = new RunCondition();
                        runCondition.Id = Guid.NewGuid();
                        runCondition.RunResult = runResult;
                        runCondition.Condition = conditionVariantResult.ConditionVariant.Condition.Name;
                        runCondition.Result = conditionVariantResult.Result;
                        context.RunConditions.Add(runCondition);
                        if (conditionVariantResult.ConditionVariant.IndexParams.Length
                            != conditionVariantResult.ConditionVariant.Condition.ParamCount)
                            throw new Exception("Число параметров условия и число индексов использованных не совпадает");
                        for (int k = 0; k < conditionVariantResult.ConditionVariant.IndexParams.Length; k++)
                        {

                            RunConditionParam param = new RunConditionParam()
                            {
                                Id = Guid.NewGuid(),
                                RunCondition = runCondition,
                                OrderParam = k,
                                IndexParam = conditionVariantResult.ConditionVariant.IndexParams[k]
                            };
                            context.RunConditionParams.Add(param);
                        }
                    }
                }

                context.SaveChanges();
            }
        }

    }
}
