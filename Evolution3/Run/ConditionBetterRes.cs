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
    /// Лучшее решение по условию.
    /// </summary>
    public class ConditionBetterRes
    {
        public IIf Condition { get; private set; }
        private readonly int _countParamsIncome;
        private readonly ConditionSet _conditionSet;
        /// <summary>
        /// Все возможные комбинации параметров
        /// </summary>
        public ISetParam SetParam { get; }

        /// <summary>
        /// Все результаты выполнения.
        /// Первый индекс - Набор параметров
        /// Второй индекс - Число вариантов параметров. BetterIndexReshuffle - показывает какой брать для лучшего решения.
        /// </summary>
        public int[,] AllResultsRun { get; private set; }
        /// <summary>
        /// Число входящих наборов.
        /// 0 индекс AllResultsRun
        /// </summary>
        public int CountIncomeVariant { get; private set; }
        /// <summary>
        /// Число возможных комбинаций параметров.
        /// 1 индекс AllResultsRun
        /// </summary>
        public int CountVariant { get { return SetParam.CountReshuffle; } }

        public ConditionBetterRes(IIf condition, int countParamsIncome)
        {
            Condition = condition;
            _countParamsIncome = countParamsIncome;
            SetParam = Setup.GetISetParam(condition, countParamsIncome);
            _conditionSet = new ConditionSet(condition);
        }


        /// <summary>
        /// Получаем массив всех выполнений на всех вариантах для данного условия.
        /// </summary>
        /// <param name="incomeVariants">
        /// Первое измерение - номер варианта
        /// Второе измерение - индекс 
        /// </param>
        /// <returns></returns>
        private void Calculate(double[,] incomeVariants)
        {
            CountIncomeVariant = incomeVariants.GetLength(0);
            if (_countParamsIncome != incomeVariants.GetLength(1))
                throw new Exception("Число входных параметров не соответствует объявленным");
            //Результат выполнения функции на всех наборах.
            AllResultsRun = new int[CountIncomeVariant, CountVariant];
            for (int i = 0; i < CountIncomeVariant; i++)
            {
                double[] incomeParam = BaseFuncSet.GetOwnSetIncomeParams(incomeVariants, i);
                double[,] incomeSetParams = SetParam.GetSet(incomeParam);
                int[] resultForSet = _conditionSet.Run(incomeSetParams);
                if (CountVariant != resultForSet.Length)
                    throw new Exception("Число ответов и число вариантов не совпадает");
                for (int j = 0; j < CountVariant; j++)
                {
                    AllResultsRun[i, j] = resultForSet[j];
                }
            }
        }

        /// <summary>
        /// Устанавливаем решения.
        /// </summary>
        /// <param name="incomeVariants">
        /// Первое измерение - номер варианта
        /// Второе измерение - индекс 
        /// </param>
        /// <param name="allDecisions">Наборы всех решений</param>
        /// <returns></returns>
        public void SetDecision(double[,] incomeVariants, ref ConditionAllDecisions allDecisions)
        {
            Calculate(incomeVariants);
            for (int i = 0; i < CountVariant; i++)
            {
                //Все ответы для варианта
                int[] variant = ArrayCopy.GetArrayTo2Index(AllResultsRun, i);
                //Расмотренные значения
                List<int> lookValue = new List<int>();
                //Описание варианта
                ConditionVariant conditionVariant = new ConditionVariant(Condition, SetParam.GetIndexIncomeParams(i));
                for (int j = 0; j < CountIncomeVariant; j++)
                {
                    int result = variant[i];
                    //Проверяем, что мы ранее не рассматривали данное значение.
                    if (!(lookValue.Any(l => l == result)))
                    {
                        lookValue.Add(result);
                        bool[] decisions = new bool[CountIncomeVariant];
                        for (int k = 0; k < CountIncomeVariant; k++)
                        {
                            //TODO: Если ответов слишком мало - может не надо смотреть корреляцию?
                            decisions[k] = result == variant[k];
                        }
                        allDecisions.AddDecision(decisions, conditionVariant, result);
                    }
                }
            }
        }
    }
}
