using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using Ninject;

namespace Testing
{
    public static class NInjectSetup
    {
        public static IKernel Kernel;

        static NInjectSetup()
        {
            Kernel = new StandardKernel();

            Kernel.Bind<ICalculation>().To<Calculat>();
        }
    }
}
