﻿using System;
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
        /// <summary>
        /// Первое измерение - номер варианта
        /// Второе измерение - индекс 
        /// </summary>
        private int[,] _incomeVariants;
        private int[] _results;
        private int _countParamIndex;
        private DataDB.Setup _setup;

        //Считываем данные из БД
        public Run()
        {
            using (EvoluationContext context = new EvoluationContext())
            {
                _setup = context.Setups.First();
                _countParamIndex = _setup.CountParamIndex;

                _incomeVariants = new int[context.InputDatas.Count() / _countParamIndex, _countParamIndex];
                foreach (InputData inputData in context.InputDatas)
                {
                    _incomeVariants[inputData.IncomeIndexId, inputData.ParamIndexId] = inputData.Value;
                }

                _results = new int[context.Results.Count()];
                foreach (Result result in context.Results)
                {
                    _results[result.IncomeIndexId] = result.Value;
                }
            }
        }

        /// <summary>
        /// Запускаем с переданными данными
        /// </summary>
        /// <param name="incomeVariants"></param>
        /// <param name="results"></param>
        public Run(int[,] incomeVariants, int[] results)
        {
            _countParamIndex = incomeVariants.GetLength(1);
            _incomeVariants = incomeVariants;
            _results = results;
        }

        /// <summary>
        /// Запускаем поиск решения
        /// </summary>
        public double Search()
        {
            Step0 step0 = new Step0(_setup);

            double betterCorr = step0.Run(_incomeVariants, _results);
            return betterCorr;
        }
    }
}
