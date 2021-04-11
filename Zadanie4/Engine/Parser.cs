using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Parser
    {
        private string[] data;

        /*
         * Inicjalizacja parsera wraz ze sciezka do pliku z ktorego maja byc
         * odczytywane dane.
         * 
         * sciezka - sciezka do pliku z danymi
         */
        public Parser(string path)
        {
            data = System.IO.File.ReadAllLines(path);
        }

        /*
         * Zwrócenie tablicy zadan, ktore zawieraja w sobie odpowiednio 
         * umiejscowione czasy poszczegolnych operacji na maszynach
         * 
         * noInstance - numer instancji problemu, ktora trzeba wczytac
         */
        public int[,] LoadTasks(int noInstance)
        {
            TaskMatrix<int> tasks = new TaskMatrix<int>(new int[1,1]);
            StreamWriter outputFile = File.CreateText("output.txt");
            int instanceCounter = 0;
            int taskCounter = 0;
            int machineNumber = 0;
            bool generalRead = false;
            foreach (string line in data)
            {
                if (line.Contains("data"))
                {
                    instanceCounter++;
                    continue;
                }

                if (instanceCounter == noInstance)
                {
                    if (line == "")
                        break;

                    outputFile.WriteLine(line);
                    outputFile.Flush();

                    string[] _ = line.Split(' ');
                    if (!generalRead)
                    {
                        tasks = new TaskMatrix<int>(new int[Int32.Parse(_[0]), Int32.Parse(_[1])]);
                        machineNumber = Int32.Parse(_[1]);
                        generalRead = true;
                    }
                    else
                    {
                        for (int i = 0; i < machineNumber; i++)
                        {
                            tasks.matrix[taskCounter, i] = Int32.Parse(_[i]);
                        }
                        taskCounter++;
                    }
                }
            }
            outputFile.Close();

            return tasks.matrix;
        }

        public void AppendCmaxAndSeq(int cmax, int[] sekwencja)
        {
            StreamWriter outputFile = File.AppendText("output.txt");
            outputFile.WriteLine();
            outputFile.WriteLine(cmax.ToString());
            foreach (int i in sekwencja)
                outputFile.Write(i.ToString() + " ");

            outputFile.Flush();
            outputFile.Close();
        }

        public static bool SaveTimeScore(Stopwatch sw, int cmax, TaskMatrix<int> tasksMatrix, string algName, int nrInst = 0)
        {
            TimeSpan ts = sw.Elapsed;
            StreamWriter outputFile = File.AppendText("result.txt");
            if (nrInst != 0)
            {
                outputFile.WriteLine("\n--Instancja " + nrInst.ToString() + "--\n" +
                                        "L. maszyn: " + tasksMatrix.GetRow(0).Length.ToString() +
                                        "\tL. zadan: " + tasksMatrix.GetColumn(0).Length.ToString() + "\n");
            }
            outputFile.Write(algName + ":  " + "Cmax = " + cmax.ToString() + "\tCzas = ");
            outputFile.Write("{0:00}:{1:00}.{2}\n", ts.Minutes, ts.Seconds, ts.Milliseconds);

            outputFile.Flush();
            outputFile.Close();
            return true;
        }
    }
}
