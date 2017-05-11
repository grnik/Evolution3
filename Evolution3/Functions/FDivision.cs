using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class FDivision : IFunction
    {
        public Guid Id
        {
            get
            {
                return new Guid("53004B94-21AC-4ECD-9AAE-000DDA2AE35A");
            }
        }

        public string Name
        {
            get { return "Division"; }
        }

        public int ParamCount
        {
            get { return 2; }
        }

        public bool Commutativity
        {
            get { return false; }
        }

        public double Run(params double[] paramInput)
        {
            if (paramInput.Length != ParamCount)
                throw new Exception("Число переданных параметров не соответствует числу параметров функции.");

            return paramInput[1] == 0 ? int.MaxValue : paramInput[0] / paramInput[1];
        }
    }
}
