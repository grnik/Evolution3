using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public enum FunctionName { Minus, Plus, Multiplication, Division }
    public static class FFactory
    {
        public static IFunction Create(string name)
        {
            switch (name)
            {
                case "Minus": return new FMinus();
                case "Plus": return new FPlus();
                case "Multiplication": return new FMultiplication();
                case "Division": return new FDivision();
                default: throw new ArgumentOutOfRangeException("Неизвестная функция");
            }
        }
    }
}
