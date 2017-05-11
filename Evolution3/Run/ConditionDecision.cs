using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    /// <summary>
    /// Результат выполнения условия.
    /// </summary>
    public class ConditionDecision
    {
        /// <summary>
        /// Число наборов входных параметров.
        /// Определяет кол-во решений.
        /// </summary>
        public int CountIncomeVariant { get; private set; }

        /// <summary>
        /// Массив решений.
        /// Индекс массива указывает номер входного параметра.
        /// True - данный входной параметр учитывается в решении
        /// False - нет.
        /// </summary>
        public bool[] Decision { get; set; }

        /// <summary>
        /// Т.к. к одному решению могут приводить несколько условий, то здесь хранятся все возможные варианты условий. которые приводят к одному решению.
        /// </summary>
        public List<ConditionVariantResult> ConditionVariantResults;

        public ConditionDecision(int countIncomeVariant)
        {
            CountIncomeVariant = countIncomeVariant;
            Decision = new bool[countIncomeVariant];
            ConditionVariantResults = new List<ConditionVariantResult>();
        }

        public ConditionDecision(bool[] decision, ConditionVariant conditionVariant, int result)
        {
            CountIncomeVariant = decision.Length;
            Decision = decision;
            ConditionVariantResults = new List<ConditionVariantResult>();
            ConditionVariantResults.Add(new ConditionVariantResult(conditionVariant, result));
        }

        public static bool operator ==(ConditionDecision conditionDecision, bool[] decision)
        {
            if (((object)conditionDecision == null) && ((object)decision == null))
                return true;
            if ((object)conditionDecision == null)
                return false;
            if (decision == null)
                return false;

            if (conditionDecision.CountIncomeVariant != decision.Length)
                throw new Exception("Размерности решений для всех вариантов условий должны совпадать");

            for (int i = 0; i < conditionDecision.CountIncomeVariant; i++)
            {
                if (conditionDecision.Decision[i] != decision[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(ConditionDecision conditionDecision, bool[] decision)
        {
            if (((object)conditionDecision == null) && ((object)decision == null))
                return false;
            if ((object)conditionDecision == null)
                return true;
            if (decision == null)
                return true;

            if (conditionDecision.CountIncomeVariant != decision.Length)
                throw new Exception("Размерности решений для всех вариантов условий должны совпадать");

            for (int i = 0; i < conditionDecision.CountIncomeVariant; i++)
            {
                if (conditionDecision.Decision[i] != decision[i])
                    return true;
            }

            return false;
        }
    }
}
