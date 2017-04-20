using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using Functions;

namespace Run
{
    /// <summary>
    /// Выполнение функции на наборе параметров
    /// </summary>
    public class FunctionSet
    {
        private IFunction _function;
        private ICalculation _calculation;

        public FunctionSet(string name, ICalculation calculation)
            : this(FFactory.Create(name), calculation)
        {
        }

        public FunctionSet(IFunction function, ICalculation calculation)
        {
            _function = function;
            _calculation = calculation;
        }

        /// <summary>
        /// Для набора входных параметров - возвращаем результат.
        /// </summary>
        /// <param name="inputParams"></param>
        /// <returns></returns>
        public int[] Run(int[,] inputParams)
        {
            int length = inputParams.GetLength(0);
            int[] res = new int[length];

            for (int i = 0; i < length; i++)
            {
                int[] funcParams = GetOwnSetIncomeParams(inputParams, i);
                res[i] = _function.Run(funcParams);
            }

            return res;
        }

        /// <summary>
        /// Возвращает корреляцию результатов выполнения функции и заданных ответов.
        /// </summary>
        /// <param name="compareResults">Заданные ответы</param>
        /// <param name="inputParams">Все наборы входных параметров</param>
        /// <returns></returns>
        public double Correlation(int[] compareResults, int[,] inputParams)
        {
            int count = inputParams.GetLength(0);
            if (compareResults.Length != count)
                throw new Exception("Число входных параметров и результатов сравнения не совпадают");
            int[] resultFunc = Run(inputParams);

            double res = _calculation.Correlation(compareResults, resultFunc);

            return res;
        }

        /// <summary>
        /// Копируем конкретный набор входных параметров из списка вариантов.
        /// </summary>
        /// <param name="setParams"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int[] GetOwnSetIncomeParams(int[,] setParams, int index)
        {
            int count = setParams.GetLength(1);
            int[] res = new int[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = setParams[index, i];
            }

            return res;
        }
    }
}
