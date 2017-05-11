using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    /// <summary>
    /// Вариант условия с результатом
    /// </summary>
    public class ConditionVariantResult
    {
        public ConditionVariant ConditionVariant { get; set; }
        public int Result { get; set; }

        public ConditionVariantResult(ConditionVariant conditionVariant, int result)
        {
            ConditionVariant = conditionVariant;
            Result = result;
        }
    }
}
