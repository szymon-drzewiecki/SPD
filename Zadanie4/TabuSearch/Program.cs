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
            int nrMetodySasiedztwa = 1;
            int liczbaIteracji = 5000;
            int nrInstancji = 1;
            int liczbaBadanychInstancji = 20;
            #endregion

            Parser parser = new Parser("neh_data.txt");
            Stopwatch[] stopwatches = new Stopwatch[2] { new Stopwatch(), new Stopwatch() };
            #region Badanie algorytmow
            for(int i = nrInstancji; i<liczbaBadanychInstancji+1; i++)
            {
                int[,] taskMatrix = parser.LoadTasks(i);

                #region Generowanie poczatkowej kolejnosci
                int[] startingPoint = new int[taskMatrix.GetLength(0)];
                for (int j = 0; j < startingPoint.Length; j++)
                    startingPoint[j] = j + 1;
                #endregion

                //Neh
                List<Tuple<int, int>> pP = Algorithms.ZwrocPosortowanePriorytety(taskMatrix);
                
                stopwatches[0].Start();
                int[] seqNeh = Neh.NehBasic(taskMatrix, pP);
                stopwatches[0].Stop();

                int cMaxNeh = Algorithms.calculateTotalspan(taskMatrix, seqNeh);

                //Tabu Search
                stopwatches[1].Start();
                int[] seqTs = TabuSearch.tabuSearch(taskMatrix, startingPoint, nrMetodySasiedztwa, liczbaIteracji);
                stopwatches[1].Stop();

                int cMaxTs = Algorithms.calculateTotalspan(taskMatrix, seqTs);

                //Zapisywanie wynikow
                int[] cMaxes = { cMaxNeh, cMaxTs };
                string[] aNames = { "Neh", "TS" };
                Parser.SaveTimeScore(stopwatches, i, cMaxes, taskMatrix, aNames, i);

                //Resetowanie stoperow
                foreach (Stopwatch sw in stopwatches)
                    sw.Reset();
            }
            #endregion
        }
    }
}
