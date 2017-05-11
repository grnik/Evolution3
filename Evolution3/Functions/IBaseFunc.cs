using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public interface IBaseFunc
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
    }
}
