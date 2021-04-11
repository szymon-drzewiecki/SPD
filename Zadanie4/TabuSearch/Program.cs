using Engine;
using System;

namespace TabuSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser("neh_data.txt");
            TaskMatrix<int> m = new TaskMatrix<int>(parser.LoadTasks(2));

            m.PrintTaskMatrix();
        }
    }
}
