using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDB;

namespace DataDB
{
    public class DBFilling
    {
        private EvoluationContext _context;

        public DBFilling()
        {
            _context = new EvoluationContext();
        }

        ~DBFilling()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Отчистить БД от результатов и входных параметров.
        /// </summary>
        public void ClearStartParam()
        {
            _context.Setups.RemoveRange(_context.Setups);
            _context.Results.RemoveRange(_context.Results);
            _context.InputDatas.RemoveRange(_context.InputDatas);
            _context.SaveChanges();
        }

        /// <summary>
        /// Отчистить БД от результатов выполнения.
        /// </summary>
        public void ClearResult()
        {
            _context.RunResultParams.RemoveRange(_context.RunResultParams);
            _context.RunResults.RemoveRange(_context.RunResults);
            _context.SaveChanges();
        }

        /// <summary>
        /// По входным параметрам считаем результат.
        /// x2 * y2 - (x + y)2 - z2
        /// </summary>
        /// <param name="incomeParam"></param>
        /// <returns></returns>
        public double FunctionCalc(params double[] incomeParam)
        {
            double res = Math.Pow(incomeParam[0], 2) * Math.Pow(incomeParam[1], 2)
                         - Math.Pow(incomeParam[0] + incomeParam[1], 2)
                         - Math.Pow(incomeParam[2], 2);

            return res;
        }

        /// <summary>
        /// По функции заполняем БД
        /// </summary>
        public void Filling()
        {
            ClearStartParam();

            Setup setup = new Setup() { Id = 0, CountParamIndex = 3, MaxLevel = 10, TargetCorrelation = 0.9999999 };
            _context.Setups.Add(setup);

            const int count = 12;
            double[] x = new double[count] { 4, 7, 8, 10, 23, 32, 43, 46, 65, 76, 78, 87 };
            double[] y = new double[count] { 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 15, 17 };
            double[] z = new double[count] { 10, 11, 12, 13, 14, 14, 16, 17, 18, 19, 20, 21 };
            double[] res = new double[count];

            for (int i = 0; i < count; i++)
            {
                AddParam(i, 0, x[i]);
                AddParam(i, 1, y[i]);
                AddParam(i, 2, z[i]);

                res[i] = FunctionCalc(new double[3] { x[i], y[i], z[i] });

                AddResult(i, res[i]);
            }
            _context.SaveChanges();
        }

        void AddParam(int incomeIndex, int paramIndex, double value)
        {
            InputData inputData = new InputData();
            inputData.IncomeIndexId = incomeIndex;
            inputData.ParamIndexId = paramIndex;
            inputData.Value = value;
            _context.InputDatas.Add(inputData);
        }

        void AddResult(int index, double res)
        {
            Result result = new Result();
            result.IncomeIndexId = index;
            result.Value = res;
            _context.Results.Add(result);
        }
    }
}
