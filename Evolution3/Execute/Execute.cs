using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDB;
using Functions;

namespace Execute
{
    /// <summary>
    /// Выполнение алгоритма.
    /// </summary>
    public class Execute
    {
        private readonly List<RunResult> _runs;
        private readonly int _maxLevel;

        /// <summary>
        /// 
        /// </summary>
        public Execute()
        {
            using (EvoluationContext context = new EvoluationContext())
            {
                _runs = context.RunResults.Include("Parameters").Include("Conditions").Include("Conditions.Parameters").OrderBy(r => r.Level).ThenByDescending(r => r.Result).ToList();
                //foreach (RunResult runResult in _runs)
                //{
                //    foreach (RunCondition runCondition in runResult.Conditions)
                //    {
                //        var parameters = runCondition.Parameters;
                //    }
                //}
                _maxLevel = _runs[_runs.Count() - 1].Level;
            }
        }

        public static double ExecuteFunction(RunResult runResult, double[] incomeParams)
        {
            IFunction function = FFactory.Create(runResult.Function);

            if (runResult.Parameters.Count != function.ParamCount)
                throw new Exception("Число параметров функции и заданных не совпадает");
            double[] functionParam = new double[function.ParamCount];
            foreach (RunResultParam runResultParam in runResult.Parameters)
            {
                functionParam[runResultParam.OrderParam] = incomeParams[runResultParam.IndexParam];
            }

            return function.Run(functionParam);
        }

        public static int ExecuteCondition(RunCondition runCondition, double[] incomeParams)
        {
            IIf condition = IfFactory.Create(runCondition.Condition);

            if (runCondition.Parameters.Count != condition.ParamCount)
                throw new Exception("Число параметров функции и заданных не совпадает");
            double[] conditionParams = new double[condition.ParamCount];
            foreach (var runResultParam in runCondition.Parameters)
            {
                conditionParams[runResultParam.OrderParam] = incomeParams[runResultParam.IndexParam];
            }

            return condition.Run(conditionParams);
        }

        /// <summary>
        /// Получения результата по решению сохраненному в БД.
        /// </summary>
        /// <param name="incomeParams"></param>
        /// <returns></returns>
        public double Run(ref double[] incomeParams)
        {
            //for (int i = 0; i < _maxLevel; i++)
            //{
            //    incomeParams[_runs[i].IndexOut] = ExecuteFunction(_runs[i], incomeParams);
            //}
            for (int i = 0; i < _maxLevel; i++)
            {
                RunLevel(ref incomeParams, i);
            }

            return incomeParams[_runs[_maxLevel - 1].IndexOut];
        }

        /// <summary>
        /// Выполнение функций с условиями определенного уровня
        /// </summary>
        /// <param name="incomeParams"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public double RunLevel(ref double[] incomeParams, int level)
        {
            RunResult[] runResults = _runs.Where(r => r.Level == level).OrderByDescending(r => r.Result).ToArray();

            return RunResults(runResults, ref incomeParams);
        }

        /// <summary>
        /// Выполняем функции с условиями.
        /// </summary>
        /// <param name="runResults"></param>
        /// <param name="incomeParams"></param>
        /// <returns></returns>
        public static double RunResults(RunResult[] runResults, ref double[] incomeParams)
        {
            for (int i = 0; i < runResults.Length; i++)
            {
                if ((runResults[i].Conditions == null)
                    || (runResults[i].Conditions.Count == 0)
                    || CheckCondition(incomeParams, runResults[i].Conditions))
                {
                    double res = ExecuteFunction(runResults[i], incomeParams);
                    incomeParams[runResults[i].IndexOut] = res;
                    return res;
                }
            }
            throw new Exception("Для списка функций и входных параметров нет ни одного решения");
        }

        /// <summary>
        /// Проверяем условия. Если хотя бы одно истинно. то возвращаем истину, иначе нет.
        /// </summary>
        /// <param name="incomeParams"></param>
        /// <param name="runConditions"></param>
        /// <returns></returns>
        public static bool CheckCondition(double[] incomeParams, ICollection<RunCondition> runConditions)
        {
            return runConditions.Any(c => ExecuteCondition(c, incomeParams) == c.Result);
            //foreach (RunCondition runCondition in runConditions)
            //{
            //    if (ExecuteCondition(runCondition, incomeParams) == runCondition.Result)
            //        return true;
            //}
            //return false;
        }
    }
}
