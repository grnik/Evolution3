using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    /// <summary>
    /// Все варианты решений условий.
    /// </summary>
    public class ConditionAllDecisions
    {
        /// <summary>
        /// Число наборов входных параметров.
        /// Определяет кол-во решений.
        /// </summary>
        public int CountIncomeVariant { get; private set; }

        public List<ConditionDecision> Decisions;

        public ConditionAllDecisions(int countIncomeVariant)
        {
            CountIncomeVariant = countIncomeVariant;
            Decisions = new List<ConditionDecision>();
        }

        public void AddDecision(bool[] decision, ConditionVariant variant, int result)
        {
            ConditionDecision conditionDecision = Decisions.FirstOrDefault(d => d == decision);
            if (conditionDecision == null)
            {
                Decisions.Add(new ConditionDecision(decision, variant, result));
            }
            else
            {
                conditionDecision.ConditionVariantResults.Add(new ConditionVariantResult(variant, result));
            }
        }
    }
}
