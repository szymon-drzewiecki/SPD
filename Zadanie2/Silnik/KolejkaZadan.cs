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
        public int liczbaMaszyn;

        public KolejkaZadan(string path, int instanceNo)
        {
            string[] neh_data = System.IO.File.ReadAllLines(path);
            int licznik_instancji = 0;
            int licznik_zadan = 0;
            bool odczytanoOgolne = false;
            foreach(string linia in neh_data)
            {
                if (linia.Contains("data"))
                {
                    licznik_instancji++;
                    continue;
                }
                else if(linia == "")
                    break;

                if (licznik_instancji == instanceNo)
                {
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
                    }
                }
            }
        }
    }
}
