using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDB
{
    public class InputData
    {
        /// <summary>
        /// Номер набора входных данных
        /// </summary>
        public int IncomeIndexId { get; set; }
        /// <summary>
        /// Номер параметра во входных данных
        /// </summary>
        public int ParamIndexId { get; set; }
        /// <summary>
        /// Значение
        /// </summary>
        public double Value { get; set; }
    }
}
