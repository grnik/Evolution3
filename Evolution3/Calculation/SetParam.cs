﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Functions;

namespace Calculation
{
    public class SetParam : ISetParam
    {
        /// <summary>
        /// Храним наборы параметров.
        /// </summary>
        static List<ReshuffleParam> _reshuffleParams = new List<ReshuffleParam>();

        private ReshuffleParam _reshuffleParam;

        /// <summary>
        /// Число различных вариантов составления набора для данной функции и числа входный параметров.
        /// </summary>
        public int CountReshuffle
        {
            get
            {
                return _reshuffleParam.CountReshuffle;
            }
        }

        private int _countParamsIncome;
        private IFunction _function;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function">Функция, которую будем оценивать</param>
        /// <param name="countParamsIncome">Количество входных параметров.</param>
        public SetParam(IFunction function, int countParamsIncome)
        {
            _function = function;
            _countParamsIncome = countParamsIncome;
            int countParamsFunction = function.ParamCount;
            bool commutativity = function.Commutativity;

            _reshuffleParam = _reshuffleParams.FirstOrDefault(r =>
                (r.Commutativity == commutativity)
                && (r.CountParamsFunction == countParamsFunction)
                && (r.CountParamsIncome == countParamsIncome));
            if (_reshuffleParam == null)
            {
                _reshuffleParam = new ReshuffleParam(countParamsFunction, countParamsIncome, commutativity);
                _reshuffleParams.Add(_reshuffleParam);
            }
        }

        /// <summary>
        /// По набору входных параметров создаем наборы для выполнения функции
        /// </summary>
        /// <param name="paramsIncome"></param>
        /// <returns></returns>
        public int[,] GetSet(int[] paramsIncome)
        {
            int countParamsIncome = paramsIncome.Length;
            if(countParamsIncome != _countParamsIncome)
                throw new Exception("Кол-во входных параметров изменилось");

            int[,] res = new int[_reshuffleParam.CountReshuffle, _function.ParamCount];
            for (int i = 0; i < _reshuffleParam.CountReshuffle; i++)
            {
                for (int j = 0; j < _function.ParamCount; j++)
                {
                    res[i, j] = paramsIncome[_reshuffleParam.Reshuffle[i, j]];
                }
            }

            return res;
        }

        /// <summary>
        /// Возращает номера индексов входных параметров для указанного индексом набора параметров.
        /// </summary>
        /// <param name="indexParamSet"></param>
        /// <returns></returns>
        public int[] GetIndexIncomeParams(int indexParamSet)
        {
            int[] res = new int[_function.ParamCount];
            for (int i = 0; i < _function.ParamCount; i++)
            {
                res[i] = _reshuffleParam.Reshuffle[indexParamSet, i];
            }

            return res;
        }

    }
}
