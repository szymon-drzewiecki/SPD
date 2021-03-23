using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik
{
    public class Algorytmy
    {
        public int calculateTotalspan(KolejkaZadan kolejka, int[] kolejnoscZadan)
        {
            int Cmax = 0;
            var liczbaZadan = kolejka.zadania.Length;
            var liczbaMaszyn = kolejka.zadania[0].ZwrocLiczbeMaszyn();
            int[] tmpHolder = new int[liczbaMaszyn];

            int[,] macierzZadan = new int[liczbaZadan, liczbaMaszyn];

            for (int x = 0; x < liczbaZadan; x++)
            {
                tmpHolder = kolejka.zadania[x].ZwrocCzasy();
                for (int y = 0; y < liczbaMaszyn; y++)
                {
                    macierzZadan[x, y] = tmpHolder[y];
                }
            }

            int liczbaIteracji = liczbaZadan + liczbaMaszyn - 1;
            List<int>[] Lista = new List<int>[liczbaIteracji];

            for (int z = 0; z < liczbaIteracji; z++)
            {
                Cmax += Lista[z].Max();
            }
            
            return Cmax;
        }

        public int[] PrzegladZupelny(KolejkaZadan kolejka)
        {
            var liczbaZadan = kolejka.zadania.Length;
            int[] macierzPermutacji = new int[liczbaZadan];

            for (int x = 0; x < liczbaZadan; x++)
            {
                macierzPermutacji[x] = x+1;
            }


        }
    }
}
