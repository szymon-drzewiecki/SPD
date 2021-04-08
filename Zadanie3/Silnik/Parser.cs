using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik
{
    public class Parser
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
            StreamWriter plikWyjsciowy = File.CreateText("output.txt");
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

                    plikWyjsciowy.WriteLine(linia);
                    plikWyjsciowy.Flush();

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
            plikWyjsciowy.Close();

            return zadania;
        }

        public void Dopisz(int cmax, int[] sekwencja)
        {
            StreamWriter plikWyjsciowy = File.AppendText("output.txt");
            plikWyjsciowy.WriteLine();
            plikWyjsciowy.WriteLine(cmax.ToString());
            foreach (int i in sekwencja)
                plikWyjsciowy.Write(i.ToString() + " ");

            plikWyjsciowy.Flush();
            plikWyjsciowy.Close();
        }

        public static bool ZapiszWynik(Stopwatch sw, int cmax, KolejkaZadan kz, string algName, int nrInst = 0)
        {
            TimeSpan ts = sw.Elapsed;
            StreamWriter plikWyjsciowy = File.AppendText("result.txt");
            if (nrInst != 0)
            {
                plikWyjsciowy.WriteLine("\n--Instancja " + nrInst.ToString() + "--\n" +
                                        "L. maszyn: " + kz.zadania[0].czasyOperacji.Length.ToString() +
                                        "\tL. zadan: " + kz.zadania.Length.ToString() + "\n");
            }
            plikWyjsciowy.Write(algName + ":  " + "Cmax = " + cmax.ToString() + "\tCzas = ");
            plikWyjsciowy.Write("{0:00}:{1:00}.{2}\n", ts.Minutes, ts.Seconds, ts.Milliseconds);

            plikWyjsciowy.Flush();
            plikWyjsciowy.Close();
            return true;
        }
    }
}
