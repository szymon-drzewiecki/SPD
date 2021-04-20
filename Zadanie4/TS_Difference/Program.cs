using Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TS_console
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Parametry poczatkowe
            int liczbaIteracji = 5000;
            int nrInstancji = 1;
            int liczbaBadanychInstancji = 20;
            #endregion

            Parser parser = new Parser("neh_data.txt");
            Stopwatch[] stopwatches = new Stopwatch[3] { new Stopwatch(), new Stopwatch(), new Stopwatch() };
            #region Badanie algorytmow - poszczegolne algorytmy
            for (int i = nrInstancji; i < liczbaBadanychInstancji + 1; i++)
            {
                int[,] taskMatrix = parser.LoadTasks(i);

                #region Generowanie poczatkowej kolejnosci - natural
                int[] startingPointN = new int[taskMatrix.GetLength(0)];
                for (int j = 0; j < startingPointN.Length; j++)
                    startingPointN[j] = j + 1;
                #endregion

                #region Generowanie poczatkowej kolejnosci - randShuffle
                int[] startingPointR = Algorithms.shuffleOrder(startingPointN);
                #endregion

                #region Generowanie poczatkowej kolejnosci - neh
                List<Tuple<int, int>> pP = Algorithms.ZwrocPosortowanePriorytety(taskMatrix);
                int[] startingPointNe = Neh.NehBasic(taskMatrix, pP);
                #endregion

                //Tabu Search - natural, neighbourhood 1
                stopwatches[0].Start();
                int[] seqTsN1 = TabuSearch.tabuSearch(taskMatrix, startingPointN, 1, liczbaIteracji);
                stopwatches[0].Stop();
                int cMaxTsN1 = Algorithms.calculateTotalspan(taskMatrix, seqTsN1);

                //Tabu Search - randShuffle, neighbourhood 1
                stopwatches[1].Start();
                int[] seqTsR1 = TabuSearch.tabuSearch(taskMatrix, startingPointR, 1, liczbaIteracji);
                stopwatches[1].Stop();
                int cMaxTsR1 = Algorithms.calculateTotalspan(taskMatrix, seqTsR1);

                //Tabu Search - neh, neighbourhood 1
                stopwatches[2].Start();
                int[] seqTsNe1 = TabuSearch.tabuSearch(taskMatrix, startingPointNe, 1, liczbaIteracji);
                stopwatches[2].Stop();
                int cMaxTsNe1 = Algorithms.calculateTotalspan(taskMatrix, seqTsNe1);

                //Zapisywanie wynikow
                int[] cMaxes = { cMaxTsN1, cMaxTsR1, cMaxTsNe1 };
                string[] aNames = { "Natural_1", "RandShuffle_1", "Neh_1" };
                Parser.SaveTimeScore("result.txt", stopwatches, i, cMaxes, taskMatrix, aNames, i);

                //Resetowanie stoperow
                foreach (Stopwatch sw in stopwatches)
                    sw.Reset();
            }
            #endregion
        }
    }
}
