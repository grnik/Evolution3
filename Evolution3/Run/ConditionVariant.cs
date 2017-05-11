using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functions;

namespace Run
{
    /// <summary>
    /// Вариант условия.
    /// Храним условие и порядковый номер входного параметра, используемый для взятия значения в условии.
    /// </summary>
    public class ConditionVariant
    {
        /// <summary>
        /// Условие
        /// </summary>
        public IIf Condition { get; private set; }
        /// <summary>
        /// Какие входные параметры используем.
        /// </summary>
        public int[] IndexParams { get; private set; }

        public ConditionVariant(IIf condition, int[] indexParams)
        {
            Condition = condition;
            if(condition.ParamCount != indexParams.Length)
                throw new Exception("Число параметров условия и размерность переданного массива, не совпадает.");
            IndexParams = indexParams;
        }
    }
}
