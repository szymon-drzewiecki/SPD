using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik
{
    class PrzegladZupelny
    {
        PrzegladZupelny()
        {

        }
        public int[] Permutation(KolejkaZadan kolejka)
        {
            var liczbaZadan = kolejka.zadania.Length;
            int[] macierzPermutacji = new int[liczbaZadan];

            for (int x = 0; x < liczbaZadan; x++)
            {
                macierzPermutacji[x] = x + 1;
            }


        }
    }
}
