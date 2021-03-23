using Microsoft.VisualStudio.TestTools.UnitTesting;
using Silnik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik.Tests
{
    [TestClass()]
    public class AlgorytmyTests
    {
        [TestMethod()]
        public void calculateTotalspanTest()
        {
            KolejkaZadan kolejka = new KolejkaZadan();
            kolejka.zadania[0].czasyOperacji[0] = 1;
            kolejka.zadania[0].czasyOperacji[1] = 3;
            kolejka.zadania[0].czasyOperacji[2] = 17;
            kolejka.zadania[1].czasyOperacji[0] = 5;
            kolejka.zadania[1].czasyOperacji[1] = 1;
            kolejka.zadania[1].czasyOperacji[2] = 1;
            kolejka.zadania[2].czasyOperacji[0] = 1;
            kolejka.zadania[2].czasyOperacji[1] = 1;
            kolejka.zadania[2].czasyOperacji[2] = 1;
            int[] kolejnoscZadan = { 1, 2, 3 };
            Algorytmy test = new Algorytmy();
            int cmax = test.calculateTotalspan(kolejka, kolejnoscZadan);

            Assert.Equals(cmax, 23);
        }
    }
}