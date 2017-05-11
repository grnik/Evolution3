using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class FMultiplication : IFunction
    {
        public Guid Id
        {
            get
            {
                return new Guid("04D37383-3B86-434F-BB72-37F28BCE1CB3");
            }
        }

        public string Name
        {
            get { return "Multiplication"; }
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

            return paramInput[0] * paramInput[1];
        }
    }
}
