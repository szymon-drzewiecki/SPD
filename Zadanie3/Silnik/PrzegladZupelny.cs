using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik
{
   public class PrzegladZupelny
    {
       public PrzegladZupelny()
        {

        }
        public int[] Przeglad(KolejkaZadan kolejka)
        {
            var kolejnoscZadan = new int[kolejka.zadania.Length];
            for (int x = 0; x < kolejka.zadania.Length; x++)
            {
                kolejnoscZadan[x] = x + 1;
            }
            int[,] macierzZadan = Algorytmy.pasreMacierzZadan(kolejka, kolejnoscZadan);
            var liczbaZadan = kolejka.zadania.Length;
            int[] optymalnaKolejnosc = new int[liczbaZadan];
            int[] poczatkowaKolejnosc = new int[liczbaZadan];
            int cmax = int.MaxValue;
            for (int i=0; i < liczbaZadan; i++)
            {
                poczatkowaKolejnosc[i] = i + 1;
            }

            List<int[]> macierzPermutacji = ListToArray(returnPermutation(poczatkowaKolejnosc));
            int checkedCmax = int.MaxValue;

            foreach (int[] kolejnosc in macierzPermutacji)
            {
               checkedCmax = Algorytmy.calculateTotalspan(kolejka, macierzZadan, kolejnosc);
                if (checkedCmax < cmax)
                {
                    cmax = checkedCmax;
                    optymalnaKolejnosc = kolejnosc;
                }
            }

            return optymalnaKolejnosc;

        }
        public static List<List<int>> returnPermutation(int[] kolejkaZadan)
        {
            var macierzPermutacji = new List<List<int>>();
            return Permutation(kolejkaZadan, 0, kolejkaZadan.Length - 1, macierzPermutacji);
        }

        static List<List<int>> Permutation(int[] kolejkaZadan, int start, int stop, List<List<int>> macierzPermutacji)
        {
            if (start == stop)
            {
                macierzPermutacji.Add(new List<int>(kolejkaZadan));
            }
            else
            {
                for (var i = start; i <= stop; i++)
                {
                    Swap(ref kolejkaZadan[start], ref kolejkaZadan[i]);
                    Permutation(kolejkaZadan, start + 1, stop, macierzPermutacji);
                    Swap(ref kolejkaZadan[start], ref kolejkaZadan[i]);
                }
            }

            return macierzPermutacji;
        }

        static void Swap(ref int a, ref int b)
        {
            var temp = a;
            a = b;
            b = temp;
        }

        public static List<int[]> ListToArray(List<List<int>> initList)
        {
            List<int[]> PermutationMatrix = new List<int[]>();
            foreach (List<int> list in initList)
            {
                PermutationMatrix.Add(list.ToArray());
            }
            return PermutationMatrix;
        }
    }
}
