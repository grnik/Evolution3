using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public enum IfName { Zero, Compare }
    public class IfFactory
    {
        private static Dictionary<string, IIf> _ifs = new Dictionary<string, IIf>();

        public static IIf Create(string name)
        {
            if (!_ifs.ContainsKey(name))
            {
                switch (name)
                {
                    case "Zero":
                        _ifs.Add(name, new IfZero()); break;
                    case "Compare":
                        _ifs.Add(name, new IfCompare()); break;
                    default:
                        throw new ArgumentOutOfRangeException("Неизвестное условие");
                }
            }

            return _ifs.First(f => f.Key == name).Value;
        }

        public static List<IIf> AllIf()
        {
            List<IIf> res = new List<IIf>();

            res.Add(Create(IfName.Zero.ToString()));
            res.Add(Create(IfName.Compare.ToString()));

            return res;
        }
    }
}
