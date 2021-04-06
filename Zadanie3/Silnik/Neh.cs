using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik
{
    public class Neh
    {
        public static List<int> NehBasic(KolejkaZadan kz, List<Tuple<int, int>> tl)
        {
            List<int> sequence = new List<int>();

            sequence.Add(tl[0].Item1);

            int _counter = 0;
            foreach(Tuple<int, int> t in tl)
            {
                _counter++;
                int[] sq = new int[_counter + 1]; //stworzenie listy mozliwych dolozen ktora to jest zawsze +1 do kroku
                for(int i = 0; i<sq.Length; i++)
                {
                    List<int> clone = new List<int>(sequence); //kopia to mieszania
                    clone.Insert(i, t.Item1);//ukladanie w po kolei
                    sq[i] = Algorytmy.calculateTotalspan(kz, clone.ToArray()); //obliczenie nowego cmaxa by sprawdzic ile wynosi
                }
                int minIndex = Array.IndexOf(sq, sq.Min());
                sequence.Insert(minIndex, t.Item1);//dodanie do prawdziwej sekwencji najlepszego wariantu
            }

            return sequence;
        }
    }
}
