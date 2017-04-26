using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using Ninject;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]
    public class CalculationTest
    {
        [Test]
        public void Correlation()
        {
            ICalculation calculation = NInjectSetup.Kernel.Get<ICalculation>();

            double[] x = new double[] { 1, 2, 3 };
            double[] y = new double[] { 2, 3, 4 };

            double crr = calculation.Correlation(x, y);

            Assert.That(crr, Is.GreaterThanOrEqualTo(0));
            Assert.That(crr, Is.LessThanOrEqualTo(1));


            x = new double[] { 1, 2, 3 };
            y = new double[] { 4, 3, 1 };

            crr = calculation.Correlation(x, y);

            Assert.That(crr, Is.GreaterThanOrEqualTo(0));
            Assert.That(crr, Is.LessThanOrEqualTo(1));
        }
    }
}
