using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using Calculation;
using Functions;
using NUnit.Framework;

namespace Testing.Calculation
{
    [TestFixture]
    class SetParamTest
    {
        Random random = new Random();
        int[] GenerateIncomeParam(int count)
        {
            int[] res = new int[count];

            for (int i = 0; i < count; i++)
            {
                res[i] = random.Next(int.MinValue, Int32.MaxValue);
            }

            return res;
        }

        [TestCase(5, "Minus")]
        [TestCase(20, "Plus")]
        public void GetSet(int incomeCount, string functionName)
        {
            IFunction function = FFactory.Create(functionName);
            ISetParam setParam = new SetParam(function, incomeCount);

            int[] incomeArray = GenerateIncomeParam(incomeCount);

            int[,] paramsInts = setParam.GetSet(incomeArray);

            int[] countIncome = new int[incomeCount];
            for (int i = 0; i < paramsInts.GetLength(0); i++)
            {
                for (int j = 0; j < function.ParamCount; j++)
                {
                    Assert.IsTrue(incomeArray.Any(p => p == paramsInts[i, j]));
                }
            }
        }
    }
}
