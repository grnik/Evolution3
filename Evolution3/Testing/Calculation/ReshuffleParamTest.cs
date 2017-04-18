using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calculation;
using NUnit.Framework;

namespace Testing.Calculation
{
    [TestFixture]
    class ReshuffleParamTest
    {
        [TestCase(2, 20, true, 210)]
        [TestCase(2, 10, true, 55)]
        [TestCase(3, 2, true, 4)]
        [TestCase(2, 3, true, 6)]
        [TestCase(2, 3, false, 9)]
        public void Reshuffle(int countParamsFunction, int countParamsIncome, bool commutativity, int count)
        {
            ReshuffleParam reshuffleParam = new ReshuffleParam(countParamsFunction, countParamsIncome, commutativity);

            Assert.That(count, Is.EqualTo(reshuffleParam.CountReshuffle));
        }
    }
}
