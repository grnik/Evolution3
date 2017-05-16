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
            StepFunctions stepFunctions = new StepFunctions(_calculation);
            StepCondition stepCondition = new StepCondition(_calculation);
            int level = 0;
            do
            {
                double bettCorr = stepFunctions.Run(_incomeVariants, _results);
                //int indexForChange = stepFunctions.GetIndexChange(_incomeVariants);
                stepFunctions.SaveResult(idRun, level, null);

                //Проверка условий.
                stepCondition.Run(_incomeVariants, _results);
                stepCondition.SaveResult(idRun, level);

                //Изменяем входные параметры на новые.
                if (!stepFunctions.IndexChange.HasValue)
                    throw new Exception("Не определен индекс выходного параметра для функции");
                ChangeIncomeVariants(ref _incomeVariants, stepFunctions.FunctionBetter[stepFunctions.BetterCorrelationIndex].BetterResult, stepFunctions.IndexChange.Value);

                if ((level >= _setup.MaxLevel) || (bettCorr > _setup.TargetCorrelation))
                {
                    return bettCorr;// = Run(_incomeVariants, results, level + 1);
                }
                level++;
            } while (true);
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
    }
}
