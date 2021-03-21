﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik
{
    public class KolejkaZadan
    {
        public Zadanie[] zadania;

        public KolejkaZadan(string sciezka, int nrInstancji)
        {
            Parser parser = new Parser(sciezka);
            zadania = parser.OdczytajZadania(nrInstancji);
        }
    }
}
