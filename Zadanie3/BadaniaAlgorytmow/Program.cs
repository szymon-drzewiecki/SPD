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
            for (int i = 0; i < 20; i++)
            {
                KolejkaZadan kz = new KolejkaZadan("neh_data.txt", i + 1);
                sw.Start();
                int[] sekwencja = algorytmy.AlgorytmJohnsona(kz);
                sw.Stop();
                int Cmax = algorytmy.calculateTotalspan(kz, sekwencja);
                Parser.ZapiszWynik(sw, Cmax, kz, "Johnson", i + 1);
                sw.Reset();
            }
        }
    }
}
