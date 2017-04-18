using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class FMultiplication : IFunction
    {
        Guid IFunction.Id
        {
            get
            {
                return new Guid("04D37383-3B86-434F-BB72-37F28BCE1CB3");
            }
        }

        string IFunction.Name
        {
            get { return "Умножение"; }
        }

        public int ParamCount
        {
            get { return 2; }
        }

        public bool Commutativity
        {
            get { return true; }
        }

        int IFunction.Run(params int[] paramInput)
        {
            if (paramInput.Length != ParamCount)
                throw new Exception("Число переданных параметров не соответствует числу параметров функции.");

            return paramInput[0] * paramInput[1];
        }
    }
}
