using Engine;

namespace TabuSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser parser = new Parser("neh_data.txt");
            int[,] taskMatrix = parser.LoadTasks(2);
            
            //----------------------
            int[] startingPoint = new int[taskMatrix.GetLength(0)];
            for  (int i = 0; i  <  startingPoint.Length; i++)
                startingPoint[i] = i  +  1;
            //----------------------

            int[] wynik = Engine.TabuSearch.tabuSearch(taskMatrix, startingPoint, 1, 5000);
            for (int x = 0; x < wynik.Length; x++)
            {
                System.Console.WriteLine(wynik[x]);
            }
            int cMax = Algorithms.calculateTotalspan(taskMatrix, wynik);
            System.Console.WriteLine(cMax);
            System.Console.Read();
        }
    }
}
