using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class Neh
    {
        public static int[] NehBasic(int[,] taskMatrix, List<Tuple<int, int>> tl)
        {
            List<int> sequence = new List<int>();
            var startingTaskOrder = new int[taskMatrix.GetLength(1)];
            for (int x = 0; x < taskMatrix.GetLength(1); x++)
            {
                startingTaskOrder[x] = x + 1;
            }


            int _counter = 0;
            foreach (Tuple<int, int> t in tl)
            {

                int[] sq = new int[_counter + 1]; //stworzenie listy mozliwych dolozen ktora to jest zawsze +1 do kroku
                for (int i = 0; i < sq.Length; i++)
                {
                    List<int> clone = new List<int>(sequence); //kopia to mieszania
                    clone.Insert(i, t.Item1);//ukladanie w po kolei
                    sq[i] = Algorithms.calculateTotalspan(taskMatrix, clone.ToArray()); //obliczenie nowego cmaxa by sprawdzic ile wynosi
                }
                int minIndex = Array.IndexOf(sq, sq.Min());
                sequence.Insert(minIndex, t.Item1);//dodanie do prawdziwej sekwencji najlepszego wariantu
                _counter++;
            }

            return sequence.ToArray();

        }
    }
}
