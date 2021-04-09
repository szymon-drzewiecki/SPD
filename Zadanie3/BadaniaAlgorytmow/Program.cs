using System;
using System.Diagnostics;
using Silnik;

namespace BadaniaAlgorytmow
{
    class Program
    { 
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Algorytmy algorytmy = new Algorytmy();
            for (int i = 0; i < 60; i++)
            {
                KolejkaZadan kz = new KolejkaZadan("neh_data.txt", i + 1);
                var kolejnoscZadan = new int[kz.zadania.Length];
                for (int x = 0; x < kz.zadania.Length; x++)
                {
                    kolejnoscZadan[x] = x + 1;
                }
                int[,] macierzZadan = Algorytmy.pasreMacierzZadan(kz, kolejnoscZadan);
                sw.Start();
                int[] sekwencja = algorytmy.AlgorytmJohnsona(kz);
                sw.Stop();
                int Cmax = Algorytmy.calculateTotalspan(kz, macierzZadan, sekwencja);
                Parser.ZapiszWynik(sw, Cmax, kz, "Johnson", i + 1);
                sw.Reset();
                var posortowane = Algorytmy.ZwrocPosortowanePriorytety(kz);
                sw.Start();
                int[] sekwencja2 = Neh.NehBasic(kz, posortowane).ToArray();
                sw.Stop();
                int Cmax2 = Algorytmy.calculateTotalspan(kz, macierzZadan, sekwencja2);
                Parser.ZapiszWynik(sw, Cmax2, kz, "NEH", i + 1);
                sw.Reset();
            }
        }
    }
}
