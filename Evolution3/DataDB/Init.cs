using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDB
{
    /// <summary>
    /// Инициализируем БД.
    /// </summary>
    public static class Init
    {
        /// <summary>
        /// Берем простой алгоритм (X + Y)*(X - 4)
        /// </summary>
        public static void Fill()
        {
            InputData[] inputDatas = new InputData[] {
                  new InputData() {IncomeIndexId = 1, ParamIndexId = 1, Value = 1 }
                , new InputData() {IncomeIndexId = 1, ParamIndexId = 2, Value = 1 } };

            Result[] results = new Result[]
            {
                new Result() {IncomeIndexId = 1, Value = -6}
            };

            using (EvoluationContext context = new EvoluationContext())
            {
                context.InputDatas.AddRange(inputDatas);
                context.Results.AddRange(results);
            }
        }

    }
}
