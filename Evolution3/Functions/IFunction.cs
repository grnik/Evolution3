using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public interface IFunction
    {
        Guid Id { get; }
        /// <summary>
        /// Название функции
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Число параметров
        /// </summary>
        int ParamCount { get; }

        /// <summary>
        /// Коммутативность и ассоциативность функции
        /// </summary>
        bool Commutativity { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramInput"></param>
        /// <returns></returns>
        int Run(params int[] paramInput);
    }
}
