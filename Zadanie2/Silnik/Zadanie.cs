using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik
{
    public class Zadanie
    {
        public int[] czasyOperacji;

        public Zadanie(int liczbaMaszyn)
        {
            czasyOperacji = new int[liczbaMaszyn];
        }

        public void UstawCzasy(int[] odczytaneCzasy)
        {
            for (int i = 0; i < odczytaneCzasy.Length; i++)
                czasyOperacji[i] = odczytaneCzasy[i];
        }

        public int[] ZwrocCzasy()
        {
            return czasyOperacji;
        }

        public int ZwrocLiczbeMaszyn()
        {
            return czasyOperacji.Length;
        }
    }
}
