using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDB
{
    public class RunResultParam
    {
        public Guid Id { get; set; }
        public Guid RunResultId { get; set; }
        public RunResult RunResult { get; set; }

        /// <summary>
        /// Порядок данного параметра при вызове функции
        /// </summary>
        public int OrderParam { get; set; }
        /// <summary>
        /// Значение какого входного параметра берем.
        /// </summary>
        public int IndexParam { get; set; }

        /// <summary>
        /// Сохраняем данные
        /// </summary>
        /// <param name="context"></param>
        public void Save(EvoluationContext context)
        {
            RunResultParam runResult = context.RunResultParams.FirstOrDefault(r => r.Id == Id);
            if (runResult == null)
            {
                context.RunResultParams.Add(this);
            }
            else
            {
                runResult.RunResultId = RunResultId;
                runResult.RunResult = RunResult;
                runResult.OrderParam = OrderParam;
                runResult.IndexParam = IndexParam;
            }
        }
    }
}
