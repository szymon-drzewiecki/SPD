using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class TabuSearch
    {
        static public int[] tabuSearch(int [,] taskMatrix, int[] startingPoint, int methodNumber, int numOfIterations)
        {
            int currentCmax = Algorithms.calculateTotalspan(taskMatrix, startingPoint);
            int theBestCmax = currentCmax;
            int[] currentPoint = startingPoint;
            List<int[]> neighbourhoods = new List<int[]>();
            List<int[]> tabuList = new List<int[]>();
            int[] theBestSoFar = new int[startingPoint.Length];
            int x = 0;

            while (x < numOfIterations)
            {
                switch (methodNumber)
                {
                    case 1:
                        neighbourhoods = generateNeighbourhoodsMethod1(currentPoint, x);
                        break;

                    case 2:
                        neighbourhoods = generateNeighbourhoodsMethod2(currentPoint);
                        break;
                }

                int[] cmaxListInGeneration = new int[neighbourhoods.Count()];
                for (int i = 0; i < neighbourhoods.Count(); i++)
                {
                    cmaxListInGeneration[i] = Algorithms.calculateTotalspan(taskMatrix, neighbourhoods[i]);
                }

                while (tabuList.Contains(neighbourhoods[Array.IndexOf(cmaxListInGeneration, cmaxListInGeneration.Min())]))
                {
                    cmaxListInGeneration[Array.IndexOf(cmaxListInGeneration, cmaxListInGeneration.Min())] = Int32.MaxValue;
                }

                currentPoint = neighbourhoods[Array.IndexOf(cmaxListInGeneration, cmaxListInGeneration.Min())];
                currentCmax = cmaxListInGeneration[Array.IndexOf(cmaxListInGeneration, cmaxListInGeneration.Min())];
                tabuList.Add(currentPoint);

                if (currentCmax < theBestCmax)
                {
                    theBestCmax = currentCmax;
                    theBestSoFar = currentPoint;
                }
                else
                {
                    x++;
                }
            }

            return theBestSoFar;
        }

        //Metoda 1 - autorski pomysł
        static private List<int []> generateNeighbourhoodsMethod1(int[] currentPosition, int x)
        {
            List<int[]> neighbourhoods = new List<int[]>();
            int changingNumber = (x + 1) % (currentPosition.Length-1);
            changingNumber++;

            for (int i = 0; i < currentPosition.Length; i++)
            {
                if (currentPosition[i] == changingNumber) { }
                else
                {
                    int[] arrClone = (int[])currentPosition.Clone();
                    int swapIndex = Array.IndexOf(currentPosition, changingNumber);
                    int tmpHolder = currentPosition[i];
                    arrClone[i] = changingNumber;
                    arrClone[swapIndex] = tmpHolder;
                    neighbourhoods.Add(arrClone);
                }
            }

            return neighbourhoods;
        }

        //Metoda 2 - cebulka
        static private List<int[]> generateNeighbourhoodsMethod2(int[] currentPosition)
        {
            List<int[]> neighbourhoods = new List<int[]>();

            for(int i = 0; i<currentPosition.Length; i++)
            {
                if(i != (currentPosition.Length - 1 - i))
                {
                    int[] _tmp = (int[])currentPosition.Clone();
                    int _tmpInt = _tmp[i];
                    _tmp[i] = _tmp[_tmp.Length - i];
                    _tmp[_tmp.Length - 1 - i] = _tmpInt;

                    neighbourhoods.Add(_tmp);
                }
            }
            return neighbourhoods;
        }
    }
}
