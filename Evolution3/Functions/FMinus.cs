using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class FMinus : IFunction
    {
        Guid IFunction.Id
        {
            get
            {
                return new Guid("0ECE788D-0B01-430E-BFD5-4DD662673092");
            }
        }

        string IFunction.Name
        {
            get { return "Minus"; }
        }

        public int ParamCount
        {
            get { return 2; }
        }

        public bool Commutativity
        {
            get { return false; }
        }

        int IFunction.Run(params int[] paramInput)
        {
            if (paramInput.Length != ParamCount)
                throw new Exception("Число переданных параметров не соответствует числу параметров функции.");

            return paramInput[0] - paramInput[1];
        }
    }
}
