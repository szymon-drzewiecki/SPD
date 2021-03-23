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
            var liczbaMaszyn = kolejka.zadania[0].czasyOperacji.Length;

            int[,] macierzZadan = new int[liczbaZadan, liczbaMaszyn];

            for (int x = 0; x < liczbaZadan; x++)
            {
                for (int y = 0; y < liczbaMaszyn; y++)
                {
                    macierzZadan[x, y] = kolejka.zadania[x].czasyOperacji[y];
                }
            }

            int[,] cMaxMatrix = new int[liczbaZadan+1, liczbaMaszyn+1];
            for (int i = 1; i < liczbaMaszyn+1; i++)
            {
                for (int z = 1; z < liczbaZadan+1; z++)
                {
                    if ( cMaxMatrix [i-1, z] > cMaxMatrix [i, z - 1])
                    {
                        cMaxMatrix[i, z] += cMaxMatrix[i - 1, z] + macierzZadan[kolejnoscZadan[i - 1]-1, z - 1];
                    }
                    else
                    {
                        cMaxMatrix[i, z] += cMaxMatrix[i, z - 1] + macierzZadan[kolejnoscZadan[i - 1] - 1, z - 1];
                    }
                }
            }

            Cmax = cMaxMatrix[liczbaZadan, liczbaMaszyn];
            return Cmax;
        }

        
    }
}
