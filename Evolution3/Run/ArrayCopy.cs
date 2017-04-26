using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run
{
    public static class ArrayCopy
    {
        /// <summary>
        /// Выбираем один вариант
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T[] GetArrayTo2Index<T>(T[,] array, int index)
        {
            int count = array.GetLength(0);
            T[] res = new T[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = array[i, index];
            }

            return res;
        }

        /// <summary>
        /// Выбираем один вариант
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T[] GetArrayTo1Index<T>(T[,] array, int index)
        {
            int count = array.GetLength(1);
            T[] res = new T[count];
            for (int i = 0; i < count; i++)
            {
                res[i] = array[index, i];
            }

            return res;
        }
    }
}
