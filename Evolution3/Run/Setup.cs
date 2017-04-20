using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using Functions;

namespace Run
{
    public static class Setup
    {
        public static ICalculation GetICalculation()
        {
            return new Calculat();
        }
        public static ISetParam GetISetParam(IFunction function, int countParamsIncome)
        {
            return new SetParam(function, countParamsIncome);
        }
    }
}
