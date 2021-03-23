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

            int[,] cMaxMatrix = new int[liczbaZadan+1, liczbaMaszyn+1];
            for (int i = 1; i < liczbaMaszyn; i++)
            {
                for (int z = 1; z < liczbaZadan; z++)
                {
                    if ( cMaxMatrix [i-1, z] > cMaxMatrix [i, z - 1])
                    {
                        cMaxMatrix[i, z] += cMaxMatrix[i - 1, z] + macierzZadan[i - 1, z - 1];
                    }
                    else
                    {
                        cMaxMatrix[i, z] += cMaxMatrix[i, z - 1] + macierzZadan[i - 1, z - 1];
                    }
                }
            }

            Cmax = cMaxMatrix[liczbaZadan + 1, liczbaMaszyn + 1];
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
