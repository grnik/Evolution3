using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class FMinus1 : IFunction
    {
        Guid IFunction.Id
        {
            get
            {
                return new Guid("15C785A2-56BD-4ED9-9A49-3D1653457A3C");
            }
        }

        string IFunction.Name
        {
            get { return "Minus1"; }
        }

        public int ParamCount
        {
            get { return 1; }
        }

        public bool Commutativity
        {
            get { return true; }
        }

        int IFunction.Run(params int[] paramInput)
        {
            if (paramInput.Length != ParamCount)
                throw new Exception("Число переданных параметров не соответствует числу параметров функции.");

            return paramInput[0] - 1;
        }
    }
}
