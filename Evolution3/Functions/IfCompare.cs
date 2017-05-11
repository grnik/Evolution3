using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    //Сравниваем два значения
    public class IfCompare : IIf
    {
        public Guid Id
        {
            get
            {
                return new Guid("A4D0903C-DBCF-4281-8916-A2FF011D2ED3");
            }
        }

        public string Name
        {
            get { return "Compare"; }
        }

        public int ParamCount
        {
            get { return 2; }
        }

        public bool Commutativity
        {
            get { return false; }
        }

        public int Run(params double[] paramInput)
        {
            if (paramInput.Length != ParamCount)
                throw new Exception("Число переданных параметров не соответствует числу параметров функции.");

            return Convert.ToInt32(CompareDouble.Compare(paramInput[0], paramInput[1]));
        }
    }
}
