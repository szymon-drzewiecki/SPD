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
        private bool titleLock = false;

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
            int[,] tasks = new int[1,1];
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
                        tasks = new int[Int32.Parse(_[0]), Int32.Parse(_[1])];
                        machineNumber = Int32.Parse(_[1]);
                        generalRead = true;
                    }
                    else
                    {
                        for (int i = 0; i < machineNumber; i++)
                        {
                            tasks[taskCounter, i] = Int32.Parse(_[i]);
                        }
                        taskCounter++;
                    }
                }
            }
            outputFile.Close();

            return tasks;
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

        public static bool SaveTimeScore(string filename, Stopwatch[] sWatches, int instanceNr, int[] cMaxes,
            int[,] tasksMatrix, string[] algNames, int title = -1)
        {
            TimeSpan[] ts = new TimeSpan[algNames.Length];
            for (int i = 0; i < algNames.Length; i++)
                ts[i] = sWatches[i].Elapsed;
            StreamWriter outputFile = File.AppendText(filename);
            if (title == 1)
            {
                string headers = "NrInstancji\tl. maszyn\tl. zadan";
                {
                    string tmp = "";
                    foreach (string aN in algNames)
                    {
                        tmp += "\t"+aN + "-cMax\t" + aN + "-Time";
                    }
                    headers += tmp;
                }
                outputFile.WriteLine(headers);
            }
            outputFile.Write(instanceNr.ToString()+"\t"+ tasksMatrix.GetLength(1).ToString() + "\t"
                + tasksMatrix.GetLength(0).ToString());
            for(int i = 0; i<algNames.Length; i++)
            {
                outputFile.Write("\t"+cMaxes[i].ToString()+"\t"+ "{0:00}:{1:00}.{2}",
                    ts[i].Minutes, ts[i].Seconds, ts[i].Milliseconds);
            }
            outputFile.Write("\n");

            outputFile.Flush();
            outputFile.Close();
            return true;
        }
    }
}
