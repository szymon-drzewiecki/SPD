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
            Stopwatch[] stopwatches = new Stopwatch[4] { new Stopwatch(), new Stopwatch(), new Stopwatch(), new Stopwatch() };
            #region Badanie algorytmow - poszczegolne algorytmy
            for (int i = nrInstancji; i < liczbaBadanychInstancji + 1; i++)
            {
                int[,] taskMatrix = parser.LoadTasks(i);

                #region Generowanie poczatkowej kolejnosci - natural
                int[] startingPointN = new int[taskMatrix.GetLength(0)];
                for (int j = 0; j < startingPointN.Length; j++)
                    startingPointN[j] = j + 1;
                #endregion

                //Tabu Search - natural, neighbourhood 1
                stopwatches[0].Start();
                int[] seqTsN1 = TabuSearch.tabuSearch(taskMatrix, startingPointN, 1, liczbaIteracji);
                stopwatches[0].Stop();
                int cMaxTsN1 = Algorithms.calculateTotalspan(taskMatrix, seqTsN1);

                //Tabu Search - natural, neighbourhood 2
                stopwatches[1].Start();
                int[] seqTsN2 = TabuSearch.tabuSearch(taskMatrix, startingPointN, 2, liczbaIteracji);
                stopwatches[1].Stop();
                int cMaxTsN2 = Algorithms.calculateTotalspan(taskMatrix, seqTsN2);

                //Tabu Search - natural, neighbourhood 3
                stopwatches[2].Start();
                int[] seqTsN3 = TabuSearch.tabuSearch(taskMatrix, startingPointN, 3, liczbaIteracji);
                stopwatches[2].Stop();
                int cMaxTsN3 = Algorithms.calculateTotalspan(taskMatrix, seqTsN3);

                //Tabu Search - natural, neighbourhood 4
                stopwatches[3].Start();
                int[] seqTsN4 = TabuSearch.tabuSearch(taskMatrix, startingPointN, 4, liczbaIteracji);
                stopwatches[3].Stop();
                int cMaxTsN4 = Algorithms.calculateTotalspan(taskMatrix, seqTsN4);

                //Zapisywanie wynikow
                int[] cMaxes = { cMaxTsN1, cMaxTsN2, cMaxTsN3, cMaxTsN4 };
                string[] aNames = { "Method1", "Method2", "Method3", "Method4" };
                Parser.SaveTimeScore("result.txt", stopwatches, i, cMaxes, taskMatrix, aNames, i);

                //Resetowanie stoperow
                foreach (Stopwatch sw in stopwatches)
                    sw.Reset();
            }
            #endregion
        }
    }
}
