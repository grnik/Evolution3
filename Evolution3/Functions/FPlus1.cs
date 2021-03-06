﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class FPlus1 : IFunction
    {
        public Guid Id
        {
            get
            {
                return new Guid("2367E8E9-82CE-47A9-8E63-830C1EA160E8");
            }
        }

        public string Name
        {
            get { return "Plus1"; }
        }

        public int ParamCount
        {
            get { return 1; }
        }

        public bool Commutativity
        {
            get { return true; }
        }

        public double Run(params double[] paramInput)
        {
            if (paramInput.Length != ParamCount)
                throw new Exception("Число переданных параметров не соответствует числу параметров функции.");

            return paramInput[0] + 1;
        }
    }
}
