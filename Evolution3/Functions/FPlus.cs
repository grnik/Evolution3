using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class FPlus : IFunction
    {
        public Guid Id
        {
            get
            {
                return new Guid("81DCE1BC-9D5E-4322-9889-49EFC9836723");
            }
        }

        public string Name
        {
            get { return "Plus"; }
        }

        public int ParamCount
        {
            get { return 2; }
        }

        public bool Commutativity
        {
            get { return true; }
        }

        public double Run(params double[] paramInput)
        {
            if (paramInput.Length != ParamCount)
                throw new Exception("Число переданных параметров не соответствует числу параметров функции.");

            return paramInput[0] + paramInput[1];
        }
    }
}
