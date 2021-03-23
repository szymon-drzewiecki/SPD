using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik
{
    class Parser
    {
        private string[] dane;

        /*
         * Inicjalizacja parsera wraz ze sciezka do pliku z ktorego maja byc
         * odczytywane dane.
         * 
         * sciezka - sciezka do pliku z danymi
         */
        public Parser(string sciezka)
        {
            dane = System.IO.File.ReadAllLines(sciezka);
        }

        /*
         * Zwrócenie tablicy zadan, ktore zawieraja w sobie odpowiednio 
         * umiejscowione czasy poszczegolnych operacji na maszynach
         * 
         * nrInstancji - numer instancji problemu, ktora trzeba wczytac
         */
        public Zadanie[] OdczytajZadania(int nrInstancji)
        {
            Zadanie[] zadania = new Zadanie[3];
            int licznik_instancji = 0;
            int licznik_zadan = 0;
            int liczbaMaszyn = 0;
            bool odczytanoOgolne = false;
            foreach (string linia in dane)
            {
                if (linia.Contains("data"))
                {
                    licznik_instancji++;
                    continue;
                }

                if (licznik_instancji == nrInstancji)
                {
                    if (linia == "")
                        break;

                    Console.WriteLine(linia);

                    string[] _ = linia.Split(' ');
                    if (!odczytanoOgolne)
                    {
                        zadania = new Zadanie[Int32.Parse(_[0])];
                        liczbaMaszyn = Int32.Parse(_[1]);
                        odczytanoOgolne = true;
                    }
                    else
                    {
                        int[] tmp_czasy = new int[liczbaMaszyn];
                        for (int i = 0; i < liczbaMaszyn; i++)
                        {
                            tmp_czasy[i] = Int32.Parse(_[i]);
                        }
                        zadania[licznik_zadan] = new Zadanie(liczbaMaszyn);
                        zadania[licznik_zadan].UstawCzasy(tmp_czasy);
                        licznik_zadan++;
                    }
                }
            }
            return zadania;
        }
    }
}
