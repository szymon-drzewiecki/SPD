﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Algorithms
    {

        public static int calculateTotalspan(int[,] taskMatrix, int[] taskOrder)
        {
            int Cmax = 0;
            var liczbaZadan = taskOrder.Length;
            var liczbaMaszyn = taskMatrix.GetLength(1);
            int[,] cMaxMatrix = new int[liczbaZadan + 1, liczbaMaszyn + 1];
            for (int i = 1; i < liczbaZadan + 1; i++)
            {
                for (int z = 1; z < liczbaMaszyn + 1; z++)
                {
                    if (cMaxMatrix[i - 1, z] > cMaxMatrix[i, z - 1])
                    {
                        cMaxMatrix[i, z] += cMaxMatrix[i - 1, z] + taskMatrix[taskOrder[i - 1] - 1, z - 1];
                    }
                    else
                    {
                        cMaxMatrix[i, z] += cMaxMatrix[i, z - 1] + taskMatrix[taskOrder[i - 1] - 1, z - 1];
                    }
                }
            }

            Cmax = cMaxMatrix[liczbaZadan, liczbaMaszyn];
            return Cmax;
        }

        /*
          * Funkcja obliczajaca sumy czasow operacji dla kazdego zadania
          * a nastepnie zwracajaca tablice tych sum.
          */
        private static int[] WyznaczPriorytety(int[,] taskMatrix)
        {
            int[] priorytety = new int[taskMatrix.GetLength(0)];

            for (int i = 0; i < priorytety.Length; i++)
                priorytety[i] = Enumerable.Range(0, taskMatrix.GetLength(1))
                    .Select(x => taskMatrix[i, x]).ToArray().Sum();
            return priorytety;
        }

        /*
         * Funkcja sortujaca nierosnaco otrzymane priorytety zadan.
         * 
         * Na wyjsciu lista krotek. Krotka wyglada tak: (indeks, priorytet)
         */
        public static List<Tuple<int, int>> ZwrocPosortowanePriorytety(int[,] taskMatrix)
        {
            List<Tuple<int, int>> tl = new List<Tuple<int, int>>();
            int[] pp = WyznaczPriorytety(taskMatrix);
            for (int i = 0; i < pp.Length; i++)
                tl.Add(new Tuple<int, int>(i + 1, pp[i]));
            tl = tl.OrderByDescending(s => s.Item2).ToList();

            return tl;
        }

        public static int[] shuffleOrder (int [] anyOrder)
        {
            int[] newRandomOrder = new int[anyOrder.Length];
            Random rnd = new Random();
            newRandomOrder = anyOrder.OrderBy(x => rnd.Next()).ToArray();
            return newRandomOrder;
        }

    }
}
