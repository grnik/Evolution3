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
        /// Для наборов входных параметров - возвращаем результат.
        /// </summary>
        /// <param name="inputParams"></param>
        /// <returns></returns>
        public double[] Run(double[,] inputParams)
        {
            int length = inputParams.GetLength(0);
            double[] res = new double[length];

            for (int i = 0; i < length; i++)
            {
                double[] funcParams = BaseFuncSet.GetOwnSetIncomeParams(inputParams, i);
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
        public double CorrelationWithRun(double[] compareResults, double[,] inputParams)
        {
            int count = inputParams.GetLength(0);
            if (compareResults.Length != count)
                throw new Exception("Число входных параметров и результатов сравнения не совпадают");
            double[] resultFunc = Run(inputParams);

            double res = _calculation.Correlation(compareResults, resultFunc);

            return res;
        }

        /// <summary>
        /// Сравнения резульатата выполения и заданных результатов. Определение корреляции.
        /// </summary>
        /// <param name="compareResults">Заданные ответы</param>
        /// <param name="resultFunc">Рассчитанные результаты</param>
        /// <returns></returns>
        public double Correlation(double[] compareResults, double[] resultFunc)
        {
            return _calculation.Correlation(compareResults, resultFunc);
        }
    }
}
