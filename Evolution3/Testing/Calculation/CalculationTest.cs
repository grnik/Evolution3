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

            List<int> x = new List<int>() { 1, 2, 3 };
            List<int> y = new List<int>() { 2, 3, 4 };

            double crr = calculation.Correlation(x, y);

            Assert.That(crr, Is.GreaterThanOrEqualTo(0));
            Assert.That(crr, Is.LessThanOrEqualTo(1));


            x = new List<int>() { 1, 2, 3 };
            y = new List<int>() { 4, 3, 1 };

            crr = calculation.Correlation(x, y);

            Assert.That(crr, Is.GreaterThanOrEqualTo(0));
            Assert.That(crr, Is.LessThanOrEqualTo(1));
        }
    }
}
