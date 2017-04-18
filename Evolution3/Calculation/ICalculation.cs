using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    public interface ICalculation
    {
        double Correlation(List<int> x, List<int> y);
    }
}
