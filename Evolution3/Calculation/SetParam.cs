using System;
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

        /// <summary>
        /// Массив с перестановками всех параметров.
        /// Первое измерение - индекс возможных вариантов перестановки
        /// Второе - номер входного параметра функции.
        /// Значение - индекс из входных параметров.
        /// </summary>
        public int[,] Reshuffle
        {
            get { return _reshuffleParam.Reshuffle; }
        }

        private int _countParamsIncome;
        private IBaseFunc _function;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="function">Функция, которую будем оценивать</param>
        /// <param name="countParamsIncome">Количество входных параметров.</param>
        public SetParam(IBaseFunc function, int countParamsIncome)
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
        /// <returns>
        /// Первый параметр - вариант набора параметров
        /// Второй параметр - номер параметра функции
        /// </returns>
        public double[,] GetSet(double[] paramsIncome)
        {
            int countParamsIncome = paramsIncome.Length;
            if (countParamsIncome != _countParamsIncome)
                throw new Exception("Кол-во входных параметров изменилось");

            double[,] res = new double[_reshuffleParam.CountReshuffle, _function.ParamCount];
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
