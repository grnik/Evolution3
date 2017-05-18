using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDB
{
    /*
SELECT RR.[Id]
      ,RR.[RunId]
      ,RR.[RunTime]
      ,RR.[Function]
      ,RR.[Result]
	  ,RR.StandardDeviation
	  ,RR.IndexOut
      ,RR.[Level]
	  ,RRP.IndexParam
	  ,RRP.OrderParam
  FROM [Evoluation3].[dbo].[RunResults] RR inner join [dbo].[RunResultParams] RRP on RR.Id = RRP.RunResultId
  order by RR.Level, RR.RunTime, RRP.OrderParam
    */
    /// <summary>
    /// 
    /// </summary>
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
        public virtual ICollection<RunResultParam> Parameters { get; set; }
        public virtual ICollection<RunCondition> Conditions { get; set; }
        /// <summary>
        /// Шаг выполнения
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Порядок проверки условий.
        /// </summary>
        public int? OrderCondition { get; set; }
        /// <summary>
        /// В какой параметр записываем результат
        /// </summary>
        public int IndexOut { get; set; }

        public double StandardDeviation { get; set; }

        /// <summary>
        /// Сохраняем данные
        /// </summary>
        /// <param name="context"></param>
        public void Save(EvoluationContext context)
        {
            RunResult runResult = context.RunResults.FirstOrDefault(r => r.Id == Id);
            if (runResult == null)
            {
                context.RunResults.Add(this);
            }
            else
            {
                runResult.RunId = RunId;
                runResult.RunTime = RunTime;
                runResult.Function = Function;
                runResult.Result = Result;
                runResult.Level = Level;
                runResult.OrderCondition = OrderCondition;
                runResult.IndexOut = IndexOut;
                runResult.StandardDeviation = StandardDeviation;
            }
            foreach (RunResultParam runResultParam in Parameters)
            {
                runResultParam.Save(context);
            }
            if (Conditions != null)
                foreach (RunCondition runCondition in Conditions)
                {
                    runCondition.Save(context);
                }
        }
    }
}
