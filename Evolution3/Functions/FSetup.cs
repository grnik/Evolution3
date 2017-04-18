using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    /// <summary>
    /// Настройки функции
    /// </summary>
    public static class FSetup
    {
        public static List<IFunction> Functions = new List<IFunction>() { new FPlus(), new FMinus(), new FMultiplication(), new FDivision() };

        public static int FunctionCount { get { return Functions.Count; } }
    }
}
