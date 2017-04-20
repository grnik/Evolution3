﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class FPlus : IFunction
    {
        Guid IFunction.Id
        {
            get
            {
                return new Guid("81DCE1BC-9D5E-4322-9889-49EFC9836723");
            }
        }

        string IFunction.Name
        {
            get { return "Сложение"; }
        }

        public int ParamCount
        {
            get { return 2; }
        }

        public bool Commutativity
        {
            get { return true; }
        }

        int IFunction.Run(params int[] paramInput)
        {
            if (paramInput.Length != ParamCount)
                throw new Exception("Число переданных параметров не соответствует числу параметров функции.");

            return paramInput[0] + paramInput[1];
        }
    }
}