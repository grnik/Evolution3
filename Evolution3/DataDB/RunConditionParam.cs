using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDB
{
    public class RunConditionParam
    {
        public Guid Id { get; set; }
        public Guid RunConditionId { get; set; }
        public RunCondition RunCondition { get; set; }

        /// <summary>
        /// Порядок данного параметра при вызове условия
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
            RunConditionParam runResult = context.RunConditionParams.FirstOrDefault(r => r.Id == Id);
            if (runResult == null)
            {
                context.RunConditionParams.Add(this);
            }
            else
            {
                runResult.RunConditionId = RunConditionId;
                runResult.RunCondition = RunCondition;
                runResult.OrderParam = OrderParam;
                runResult.IndexParam = IndexParam;
            }
        }
    }
}
