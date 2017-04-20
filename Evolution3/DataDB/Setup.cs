using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDB
{
    public class Setup
    {
        public int Id { get; set; }
        /// <summary>
        /// Сколько входных параметров в каждом запросе.
        /// </summary>
        public int CountParamIndex { get; set; }
    }
}
