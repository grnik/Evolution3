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
        private static Dictionary<string, IFunction> functions = new Dictionary<string, IFunction>();

        public static IFunction Create(string name)
        {
            if (!functions.ContainsKey(name))
            {
                switch (name)
                {
                    case "Minus":
                        functions.Add(name, new FMinus()); break;
                    case "Plus":
                        functions.Add(name, new FPlus()); break;
                    case "Multiplication":
                        functions.Add(name, new FMultiplication()); break;
                    case "Division":
                        functions.Add(name, new FDivision()); break;
                    default:
                        throw new ArgumentOutOfRangeException("Неизвестная функция");
                }
            }

            return functions.First(f => f.Key == name).Value;
        }

        public static List<IFunction> AllFunction()
        {
            List<IFunction> res = new List<IFunction>();

            res.Add(Create(FunctionName.Minus.ToString()));
            res.Add(Create(FunctionName.Plus.ToString()));
            res.Add(Create(FunctionName.Multiplication.ToString()));
            res.Add(Create(FunctionName.Division.ToString()));

            return res;
        }
    }
}
