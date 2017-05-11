using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDB
{
    /// <summary>
    /// Условие выполнение конкретного шага.
    /// У шага может быть несколько наборов условий. В каждом наборе может быть несколько условий. Если хотя бы одно условие выполняется - значит выполняем данный шаг.
    /// </summary>
    public class RunCondition
    {
        public Guid Id { get; set; }
        public Guid RunResultId { get; set; }
        public RunResult RunResult { get; set; }
        public ICollection<RunConditionParam> Parameters { get; set; }
        public string Condition { get; set; }
        /// <summary>
        /// Результат при котором выполняется данное условие.
        /// </summary>
        public int Result { get; set; }

    }
}
