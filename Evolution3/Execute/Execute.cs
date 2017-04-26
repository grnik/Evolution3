using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDB;
using Functions;

namespace Execute
{
    /// <summary>
    /// Выполнение алгоритма.
    /// </summary>
    public class Execute
    {
        private List<RunResult> _runs;
        private int _count;
        private IFunction[] _functions;

        /// <summary>
        /// 
        /// </summary>
        public Execute()
        {
            using (EvoluationContext context = new EvoluationContext())
            {
                _runs = context.RunResults.Include("Parameters").OrderBy(r => r.Level).ToList();
                _count = _runs.Count();
                _functions = new IFunction[_count];
                for (int i = 0; i < _count; i++)
                {
                    _functions[i] = FFactory.Create(_runs.ElementAt(i).Function);
                }
            }
        }

        public double Run(double[] incomeParams)
        {
            for (int i = 0; i < _count; i++)
            {
                incomeParams[_runs[i].IndexOut] = _functions[i].Run(GetParams(incomeParams, _runs[i]));
            }

            return incomeParams[_runs[_count - 1].IndexOut];
        }

        double[] GetParams(double[] incomeParams, RunResult run)
        {
            int countFunc = run.Parameters.Count;
            double[] res = new double[countFunc];
            List<RunResultParam> runParams = run.Parameters.OrderBy(p => p.OrderParam).ToList();
            for (int i = 0; i < countFunc; i++)
            {
                res[i] = incomeParams[runParams[i].IndexParam];
            }

            return res;
        }
    }
}
