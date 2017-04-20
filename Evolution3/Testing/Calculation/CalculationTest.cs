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

            int[] x = new int[] { 1, 2, 3 };
            int[] y = new int[] { 2, 3, 4 };

            double crr = calculation.Correlation(x, y);

            Assert.That(crr, Is.GreaterThanOrEqualTo(0));
            Assert.That(crr, Is.LessThanOrEqualTo(1));


            x = new int[] { 1, 2, 3 };
            y = new int[] { 4, 3, 1 };

            crr = calculation.Correlation(x, y);

            Assert.That(crr, Is.GreaterThanOrEqualTo(0));
            Assert.That(crr, Is.LessThanOrEqualTo(1));
        }
    }
}
