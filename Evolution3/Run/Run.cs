using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDB;

namespace Run
{
    /// <summary>
    /// Запускаем подбор.
    /// </summary>
    public class Run
    {
        private int[,] _incomeParams;
        private int[] _results;
        private int _countParamIndex;

        //Считываем данные из БД
        public Run()
        {
            using (EvoluationContext context = new EvoluationContext())
            {
                _countParamIndex = context.Setups.First().CountParamIndex;
                _incomeParams = new int[context.InputDatas.Count() / _countParamIndex, _countParamIndex];
                _results = new int[context.Results.Count()];
            }
        }

        /// <summary>
        /// Запускаем с переданными данными
        /// </summary>
        /// <param name="incomeParams"></param>
        /// <param name="results"></param>
        public Run(int[,] incomeParams, int[] results)
        {
            _countParamIndex = incomeParams.GetLength(1);
            _incomeParams = incomeParams;
            _results = results;
        }

    }
}
