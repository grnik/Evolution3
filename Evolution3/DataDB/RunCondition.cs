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
        public virtual ICollection<RunConditionParam> Parameters { get; set; }
        public string Condition { get; set; }
        /// <summary>
        /// Результат при котором выполняется данное условие.
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// Сохраняем данные
        /// </summary>
        /// <param name="context"></param>
        public void Save(EvoluationContext context)
        {
            RunCondition runResult = context.RunConditions.FirstOrDefault(r => r.Id == Id);
            if (runResult == null)
            {
                context.RunConditions.Add(this);
            }
            else
            {
                runResult.RunResultId = RunResultId;
                runResult.RunResult = RunResult;
                runResult.Condition = Condition;
                runResult.Result = Result;
            }
            foreach (RunConditionParam runResultParam in Parameters)
            {
                runResultParam.Save(context);
            }
        }
    }
}
