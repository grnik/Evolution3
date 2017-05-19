using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using Functions;
using Ninject.Modules;

namespace Run
{
    public class NinjectConfig : NinjectModule
    {

        public override void Load()
        {
            this.Bind<ICalculation>().To<CalculateForex>();
            this.Bind<ISetParam>().To<SetParam>();
        }
    }
}
