using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class FDivision : IFunction
    {
        Guid IFunction.Id
        {
            get
            {
                return new Guid("53004B94-21AC-4ECD-9AAE-000DDA2AE35A");
            }
        }

        string IFunction.Name
        {
            get { return "Деление"; }
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

            return paramInput[0] * paramInput[1];
        }
    }
}
