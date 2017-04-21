using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculation
{
    /// <summary>
    /// Определяем перестановки параметров.
    /// </summary>
    public class ReshuffleParam
    {
        public int CountParamsFunction { get; }
        public int CountParamsIncome { get; }
        public bool Commutativity { get; }

        /// <summary>
        /// Число вариантов параметров.
        /// </summary>
        public int CountReshuffle { get; }
        /// <summary>
        /// Массив с перестановками всех параметров.
        /// Первое измерение - индекс возможных вариантов перестановки
        /// Второе - ссылка на индекс входящих значений.
        /// </summary>
        public int[,] Reshuffle { get; }

        /// <summary>
        /// Текущее значение индекса для заполнения таблицы использования параметров.
        /// </summary>
        private int _indexReshuffle;

        /// <summary>
        /// Заполняем данными по переборке значений параметров.
        /// http://umk.portal.kemsu.ru/uch-mathematics/papers/posobie/r2-3.htm
        /// </summary>
        /// <param name="countParamsFunction">Число входных параметров функции</param>
        /// <param name="countParamsIncome">Число входных параметров для решения задачи</param>
        /// <param name="commutativity">Коммутативность функции</param>
        public ReshuffleParam(int countParamsFunction, int countParamsIncome, bool commutativity)
        {
            checked
            {
                CountParamsFunction = countParamsFunction;
                CountParamsIncome = countParamsIncome;
                Commutativity = commutativity;

                if (commutativity)
                {
                    //Рачсет в лоб - переполнение.
                    //    CountReshuffle = Convert.ToInt32(FactN(countParamsFunction + countParamsIncome - 1) /
                    //                     (FactN(countParamsFunction) * FactN(countParamsIncome - 1)));
                    if (countParamsFunction > (countParamsIncome - 1))
                    {
                        CountReshuffle =
                            Convert.ToInt32(
                                FactDip(countParamsFunction + 1, countParamsFunction + countParamsIncome - 1) /
                                FactN(countParamsIncome - 1));
                    }
                    else
                    {
                        CountReshuffle =
                            Convert.ToInt32(
                                FactDip(countParamsIncome, countParamsFunction + countParamsIncome - 1) /
                                FactN(countParamsFunction));
                    }
                }
                else
                {
                    CountReshuffle = Convert.ToInt32(Math.Pow(countParamsIncome, countParamsFunction));
                }
                Reshuffle = new int[CountReshuffle, countParamsFunction];
                if (commutativity)
                {
                    CombinationsWithRepetitions();
                }
                else
                {
                    PlacementsWithRepetitions();
                }

                if (_indexReshuffle != CountReshuffle)
                    throw new Exception("При состовлении комбинации параметров возникла ошибка.");
            }
        }

        #region CombinationsWithRepetitions

        /// <summary>
        /// Сочетания с повторениями.
        /// </summary>
        void CombinationsWithRepetitions()
        {
            _indexReshuffle = 0;
            for (int i = 0; i < CountParamsIncome; i++)
            {
                int[] setParams = new int[CountParamsFunction];
                setParams[0] = i;
                CombinationsWithRepetitionsLevel(1, setParams, i);
            }
        }

        void CombinationsWithRepetitionsLevel(int level, int[] setParams, int lastIndex)
        {
            if (level == CountParamsFunction)
            {
                for (int i = 0; i < CountParamsFunction; i++)
                {
                    Reshuffle[_indexReshuffle, i] = setParams[i];
                }
                _indexReshuffle++;
                return;
            }
            for (int j = lastIndex; j < CountParamsIncome; j++)
            {
                setParams[level] = j;
                CombinationsWithRepetitionsLevel(level + 1, setParams, j);
            }
        }

        #endregion

        #region PlacementsWithRepetitions

        /// <summary>
        /// Размещений с повторениями
        /// </summary>
        void PlacementsWithRepetitions()
        {
            _indexReshuffle = 0;
            for (int i = 0; i < CountParamsIncome; i++)
            {
                int[] setParams = new int[CountParamsFunction];
                setParams[0] = i;
                PlacementsWithRepetitionsLevel(1, setParams);
            }
        }

        void PlacementsWithRepetitionsLevel(int level, int[] setParams)
        {
            if (level == CountParamsFunction)
            {
                for (int i = 0; i < CountParamsFunction; i++)
                {
                    Reshuffle[_indexReshuffle, i] = setParams[i];
                }
                _indexReshuffle++;
                return;
            }
            for (int j = 0; j < CountParamsIncome; j++)
            {
                setParams[level] = j;
                PlacementsWithRepetitionsLevel(level + 1, setParams);
            }
        }

        #endregion

        /// <summary>
        /// Факториал числа.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private long FactN(int number)
        {
            long res = 1;
            for (int i = 2; i <= number; i++)
            {
                res *= i;
            }

            return res;
        }

        /// <summary>
        /// Рассчитываем частичный факториал (не от 1, а от заданного числа).
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        private long FactDip(int start, int finish)
        {
            checked
            {
                long res = 1;
                for (int i = start; i <= finish; i++)
                {
                    res *= i;
                }

                return res;
            }
        }
    }
}
