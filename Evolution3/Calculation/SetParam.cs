using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functions;

namespace Calculation
{
    public class SetParam : ISetParam
    {
        /// <summary>
        /// Храним наборы параметров.
        /// </summary>
        static List<ReshuffleParam> _reshuffleParams = new List<ReshuffleParam>();

        public int[,] GetSet(int[] paramsIncome, IFunction function)
        {
            int countParamsFunction = function.ParamCount;
            int countParamsIncome = paramsIncome.Length;
            bool commutativity = function.Commutativity;

            ReshuffleParam reshuffleParam = _reshuffleParams.FirstOrDefault(r =>
                (r.Commutativity == commutativity)
                && (r.CountParamsFunction == countParamsFunction)
                && (r.CountParamsIncome == countParamsIncome));
            if (reshuffleParam == null)
            {
                reshuffleParam = new ReshuffleParam(countParamsFunction, countParamsIncome, commutativity);
                _reshuffleParams.Add(reshuffleParam);
            }

            int[,] res = new int[reshuffleParam.CountReshuffle, function.ParamCount];
            for (int i = 0; i < reshuffleParam.CountReshuffle; i++)
            {
                for (int j = 0; j < function.ParamCount; j++)
                {
                    res[i, j] = paramsIncome[reshuffleParam.Reshuffle[i, j]];
                }
            }

            return res;
        }
    }
}
