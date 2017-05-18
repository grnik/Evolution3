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
        /// Второе измерение - индекс конкретного значения параметра в варианте
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
            StepFunctions stepFunctions = new StepFunctions(_calculation);
            StepCondition stepCondition = new StepCondition(_calculation);
            int level = 0;
            using (EvoluationContext context = new EvoluationContext())
            {
                do
                {
                    double bettCorr = stepFunctions.Run(_incomeVariants, _results, idRun, level);
                    //int indexForChange = stepFunctions.GetIndexChange(_incomeVariants);
                    List<RunResult> runResultsLevel = new List<RunResult>() { stepFunctions.RunResult };

                    //Проверка условий.
                    stepCondition.Run(_incomeVariants, _results, idRun, level);
                    runResultsLevel.AddRange(stepCondition.RunResults);

                    double[] resultLevel = CalcResultLevel(runResultsLevel, _incomeVariants);

                    int indexForChange = GetIndexChange(_incomeVariants, resultLevel);

                    SaveResult(context, runResultsLevel);

                    //Изменяем входные параметры на новые.
                    ChangeIncomeVariants(ref _incomeVariants, resultLevel, indexForChange);

                    if ((level >= _setup.MaxLevel) || (bettCorr > _setup.TargetCorrelation))
                    {
                        return bettCorr;// = Run(_incomeVariants, results, level + 1);
                    }
                    level++;
                } while (true);
            }
        }

        private void SaveResult(EvoluationContext context, List<RunResult> runResultsLevel)
        {
            foreach (RunResult runResult in runResultsLevel)
            {
                runResult.Save(context);
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Для данного списка решений ищем ответы по входным параметрам.
        /// </summary>
        /// <param name="runResultsLevel"></param>
        /// <param name="_incomeVariants"></param>
        /// <returns></returns>
        private double[] CalcResultLevel(List<RunResult> runResultsLevel, double[,] incomeVariants)
        {
            int countRes = incomeVariants.GetLength(0);
            if (_results.Length != countRes)
                throw new Exception("Число входных параметров и число известных ответов - должны совпадать.");

            double[] res = new double[countRes];
            for (int i = 0; i < countRes; i++)
            {
                double[] incomeVariant = ArrayCopy.GetArrayTo1Index(incomeVariants, i);
                res[i] = Execute.Execute.RunResults(runResultsLevel.ToArray(), ref incomeVariant);
            }

            return res;
        }


        /// <summary>
        /// В исходных параметрах меняем данные на результат выполнения лучшей функции.
        /// </summary>
        /// <param name="incomVariants"></param>
        /// <param name="forChangeArray"></param>
        /// <param name="index"></param>
        private static void ChangeIncomeVariants(ref double[,] incomVariants, double[] forChangeArray, int index)
        {
            //TODO: Сделать, что если результат не коррелирует ни с одним параметром, то добавлять новый параметр.
            int count = incomVariants.GetLength(0);
            if (count != forChangeArray.Length)
                throw new Exception("Размерность входных параметров и результата на замену - не совпадает");
            for (int i = 0; i < count; i++)
            {
                incomVariants[i, index] = forChangeArray[i];
            }
        }

        //TODO: Расмотреть возможность добавить параметр, если корреляция с входными параметрами не слишком большая.
        /// <summary>
        /// Определяем какой входной параметр заменить. 
        /// Индекс входного параметра с наибольшей корреляцией с результатом.
        /// </summary>
        /// <param name="incomVariants">Наборы входных параметров</param>
        /// <param name="betterResult">Результат расчета для набора входных параметров</param>
        /// <returns>Индекс входного параметра для замены.</returns>
        private int GetIndexChange(double[,] incomVariants, double[] betterResult)
        {
            int count = incomVariants.GetLength(0);
            if (count != betterResult.Length)
                throw new Exception("Число входных параметров и результатов не совпадает");
            double bestCorr = 0;
            int bestIndex = 0;

            for (int i = 0; i < incomVariants.GetLength(1); i++)
            {
                double correlation = Math.Abs(_calculation.Correlation(ArrayCopy.GetArrayTo2Index(incomVariants, i), betterResult));
                if (correlation > bestCorr)
                {
                    bestIndex = i;
                    bestCorr = correlation;
                }
            }

            return bestIndex;
        }

    }
}
