using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class FMinus : IFunction
    {
        public Guid Id
        {
            get
            {
                return new Guid("0ECE788D-0B01-430E-BFD5-4DD662673092");
            }
        }

        public string Name
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

        public double Run(params double[] paramInput)
        {
            if (paramInput.Length != ParamCount)
                throw new Exception("Число переданных параметров не соответствует числу параметров функции.");

            return paramInput[0] - paramInput[1];
        }
    }
}
