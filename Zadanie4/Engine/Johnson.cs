using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Johnson
    {
        public static int[,] Transpose(int[,] taskMatrix)
        {
            int w = taskMatrix.GetLength(0);
            int h = taskMatrix.GetLength(1);

            int[,] result = new int[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = taskMatrix[i, j];
                }
            }

            return result;
        }

        public static int[] AlgorytmJohnsona(int[,] taskMatrix)
        {
            var liczbaZadan = taskMatrix.GetLength(0);
            var liczbaMaszyn = taskMatrix.GetLength(1);
            int[,] macierzZadan = new int[liczbaZadan, liczbaMaszyn];
            int[,] transMacierzZadan = new int[liczbaMaszyn, liczbaZadan];
            int[] tmpHolder = new int[liczbaMaszyn];
            int iteracja = liczbaZadan;
            int[] kolejnoscJohnsona = new int[iteracja];
            int outOfRange = 123456;

            for (int i = 0; i < liczbaZadan; i++)
            {
                tmpHolder = Enumerable.Range(0, taskMatrix.GetLength(1))
                    .Select(x => taskMatrix[i, x]).ToArray();
                for (int y = 0; y < liczbaMaszyn; y++)
                {
                    macierzZadan[i, y] = tmpHolder[y];
                }

            }


            if (liczbaMaszyn == 2)
            {
                for (int x = 0; x < liczbaMaszyn; x++)
                {
                    for (int y = 0; y < liczbaZadan; y++)
                    {
                        transMacierzZadan = Transpose(macierzZadan);
                    }
                }

                int[] kopiaMaszyny1 = new int[liczbaZadan];
                int[] kopiaMaszyny2 = new int[liczbaZadan];
                for (int i = 0; i < liczbaZadan; i++)
                {
                    kopiaMaszyny1[i] = transMacierzZadan[0, i];
                    kopiaMaszyny2[i] = transMacierzZadan[1, i];
                }
                int pozycja1, pozycja2 = liczbaZadan;

                for (int j = 0; j < iteracja; j++)
                {
                    int lowest1 = kopiaMaszyny1[0];
                    int lowest2 = kopiaMaszyny2[0];
                    pozycja1 = 0;
                    pozycja2 = 0;
                    for (int i = 1; i < liczbaZadan; i++)
                    {
                        if (kopiaMaszyny1[i] < lowest1)
                        {
                            lowest1 = kopiaMaszyny1[i];
                            pozycja1 = i;
                        }
                        if (kopiaMaszyny2[i] < lowest2)
                        {
                            lowest2 = kopiaMaszyny2[i];
                            pozycja2 = i;
                        }
                    }
                    if (lowest1 == lowest2)
                    {
                        if (pozycja1 <= pozycja2)
                        {
                            kolejnoscJohnsona[j] = pozycja1 + 1;
                            kopiaMaszyny1[pozycja1] = kopiaMaszyny2[pozycja1] = outOfRange;
                        }
                        else
                        {
                            kolejnoscJohnsona[iteracja - 1] = pozycja2 + 1;
                            iteracja--;
                            kopiaMaszyny1[pozycja2] = kopiaMaszyny2[pozycja2] = outOfRange;
                            j--;
                        }
                    }

                    if (lowest1 < lowest2)
                    {
                        kolejnoscJohnsona[j] = pozycja1 + 1;
                        kopiaMaszyny1[pozycja1] = kopiaMaszyny2[pozycja1] = outOfRange;
                    }
                    if (lowest1 > lowest2)
                    {
                        kolejnoscJohnsona[iteracja - 1] = pozycja2 + 1;
                        iteracja--;
                        kopiaMaszyny1[pozycja2] = kopiaMaszyny2[pozycja2] = outOfRange;
                        j--;
                    }
                }
            }

            else if (liczbaMaszyn > 2)
            {
                int[] kopiaMaszyny11 = new int[liczbaZadan];
                int[] kopiaMaszyny22 = new int[liczbaZadan];
                int srodek = (liczbaMaszyn + 1) / 2;
                int[] tmpHolder2 = new int[liczbaMaszyn];
                int[,] macierzZadan2 = new int[liczbaZadan, 2];
                int[,] transMacierzZadan2 = new int[2, liczbaZadan];

                int midN = liczbaMaszyn / 2;
                int midNR = liczbaMaszyn % 2;
                bool isOdd = false;

                if (midNR != 0)
                    isOdd = true;

                for (int j = 0; j < liczbaZadan; j++)
                {
                    for (int k = 0; k < liczbaMaszyn; k++)
                    {
                        if (k < midN)
                        {
                            macierzZadan2[j, 0] += macierzZadan[j, k];
                            if (isOdd && !(k + 1 < midN))
                                macierzZadan2[j, 0] += macierzZadan[j, k + 1];
                        }
                        else if (k >= midN)
                        {
                            macierzZadan2[j, 1] += macierzZadan[j, k];
                        }

                    }

                }
                for (int i = 0; i < liczbaZadan; i++)
                {
                    Console.WriteLine(macierzZadan2[i, 0] + "  " + macierzZadan2[i, 1]);
                }

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < liczbaZadan; j++)
                    {
                        transMacierzZadan2 = Transpose(macierzZadan2);
                    }
                }

                for (int i = 0; i < liczbaZadan; i++)
                {
                    kopiaMaszyny11[i] = transMacierzZadan2[0, i];
                    kopiaMaszyny22[i] = transMacierzZadan2[1, i];
                }
                int pozycja1, pozycja2 = liczbaZadan;

                for (int j = 0; j < iteracja; j++)
                {
                    int lowest1 = kopiaMaszyny11[0];
                    int lowest2 = kopiaMaszyny22[0];
                    pozycja1 = 0;
                    pozycja2 = 0;
                    for (int i = 1; i < liczbaZadan; i++)
                    {
                        if (kopiaMaszyny11[i] < lowest1)
                        {
                            lowest1 = kopiaMaszyny11[i];
                            pozycja1 = i;
                        }
                        if (kopiaMaszyny22[i] < lowest2)
                        {
                            lowest2 = kopiaMaszyny22[i];
                            pozycja2 = i;
                        }
                    }
                    if (lowest1 == lowest2)
                    {
                        if (pozycja1 <= pozycja2)
                        {
                            kolejnoscJohnsona[j] = pozycja1 + 1;
                            kopiaMaszyny11[pozycja1] = kopiaMaszyny22[pozycja1] = outOfRange;
                        }
                        else
                        {
                            kolejnoscJohnsona[iteracja - 1] = pozycja2 + 1;
                            iteracja--;
                            kopiaMaszyny11[pozycja2] = kopiaMaszyny22[pozycja2] = outOfRange;
                            j--;
                        }
                    }

                    if (lowest1 < lowest2)
                    {
                        kolejnoscJohnsona[j] = pozycja1 + 1;
                        kopiaMaszyny11[pozycja1] = kopiaMaszyny22[pozycja1] = outOfRange;
                    }
                    if (lowest1 > lowest2)
                    {
                        kolejnoscJohnsona[iteracja - 1] = pozycja2 + 1;
                        iteracja--;
                        kopiaMaszyny11[pozycja2] = kopiaMaszyny22[pozycja2] = outOfRange;
                        j--;
                    }

                }

            }
            return kolejnoscJohnsona;
        }
    }
}
