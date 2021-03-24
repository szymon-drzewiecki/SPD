using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik
{
    public class KolejkaZadan
    {
        public Zadanie[] zadania;
        Parser parser;

        public KolejkaZadan(string sciezka, int nrInstancji)
        {
            parser = new Parser(sciezka);
            zadania = parser.OdczytajZadania(nrInstancji);
        }

        public void WypiszRezultaty(int cmax, int[] sekwencja)
        {
            parser.Dopisz(cmax, sekwencja);
        }
    }
}
