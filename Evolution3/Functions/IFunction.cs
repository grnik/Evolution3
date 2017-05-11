using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public interface IFunction : IBaseFunc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramInput"></param>
        /// <returns></returns>
        double Run(params double[] paramInput);
    }
}
