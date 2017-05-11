using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    /// <summary>
    /// Сравнение с 0
    /// </summary>
    public class IfZero : IIf
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
            get { return "Zero"; }
        }

        public int ParamCount
        {
            get { return 1; }
        }

        public bool Commutativity
        {
            get { return true; }
        }

        public int Run(params double[] paramInput)
        {
            if (paramInput.Length != ParamCount)
                throw new Exception("Число переданных параметров не соответствует числу параметров функции.");

            return Convert.ToInt32(CompareDouble.Compare(paramInput[0], 0));
        }
    }
}
