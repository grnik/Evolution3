﻿using System;
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
      ,RR.[Level]
	  ,RRP.IndexParam
	  ,RRP.OrderParam
  FROM [Evoluation3].[dbo].[RunResults] RR inner join [dbo].[RunResultParams] RRP on RR.Id = RRP.RunResultId
  order by RR.Level, RRP.OrderParam
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
        public ICollection<RunResultParam> Parameters { get; set; }
        public int Level { get; set; }
        /// <summary>
        /// В какой параметр записываем результат
        /// </summary>
        public int IndexOut { get; set; }

        public double StandardDeviation { get; set; }
    }
}
