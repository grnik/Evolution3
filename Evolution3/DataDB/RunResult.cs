using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDB
{
    public class RunResult
    {
        public Guid Id { get; set; }
        /// <summary>
        /// Единый номер для всех результатов выполнения одного запуска.
        /// </summary>
        public Guid RunId { get; set; }
        /// <summary>
        /// Время запуска
        /// </summary>
        public DateTime RunTime { get; set; }
        /// <summary>
        /// Название функции
        /// </summary>
        public string Function { get; set; }
        /// <summary>
        /// Корреляция данной функции
        /// </summary>
        public double Result { get; set; }
        public ICollection<RunResultParam> Parameters { get; set; }
        public int Level { get; set; }

    }
}
