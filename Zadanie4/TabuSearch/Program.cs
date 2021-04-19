using Engine;
using System;

namespace TabuSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser("neh_data.txt");
            int[,] taskMatrix = parser.LoadTasks(2);

            
        }
    }
}
