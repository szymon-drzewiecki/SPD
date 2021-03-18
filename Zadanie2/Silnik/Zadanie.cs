using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silnik
{
    public class Zadanie
    {
        public int czasTrwania { get; set; }
        public int numerZadania { get; set; }

        public Zadanie(int y, int x)
        {
            this.czasTrwania = y;
            this.numerZadania = x;
        }
    }
}
