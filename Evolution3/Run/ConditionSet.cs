using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using Functions;

namespace Run
{
    public class ConditionSet
    {
        private IIf _condition;

        public ConditionSet(string name)
            : this(IfFactory.Create(name))
        {
        }

        public ConditionSet(IIf condition)
        {
            _condition = condition;
        }

        /// <summary>
        /// Для наборов входных параметров - возвращаем набор результатов сравнения.
        /// </summary>
        /// <param name="inputParams"></param>
        /// <returns></returns>
        public int[] Run(double[,] inputParams)
        {
            int length = inputParams.GetLength(0);
            int[] res = new int[length];

            for (int i = 0; i < length; i++)
            {
                double[] funcParams = BaseFuncSet.GetOwnSetIncomeParams(inputParams, i);
                res[i] = _condition.Run(funcParams);
            }

            return res;
        }
    }
}
