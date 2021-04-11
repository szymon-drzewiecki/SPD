using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class TaskMatrix<T>
    {
        public T[,] matrix { get; set; }

        public TaskMatrix(T[,] copyMatrix)
        {
            this.matrix = copyMatrix;
        }

        public T[] GetColumn(int columnNumber)
        {
            return Enumerable.Range(0, this.matrix.GetLength(0))
                    .Select(x => this.matrix[x, columnNumber])
                    .ToArray();
        }

        public T[] GetRow(int rowNumber)
        {
            return Enumerable.Range(0, this.matrix.GetLength(1))
                    .Select(x => this.matrix[rowNumber, x])
                    .ToArray();
        }

        public T[,] Transpose()
        {
            int w = this.matrix.GetLength(0);
            int h = this.matrix.GetLength(1);

            T[,] result = new T[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = this.matrix[i, j];
                }
            }

            return result;
        }

        public void PrintTaskMatrix()
        {
            for(int i = 0; i < this.GetColumn(0).Length; i++)
            {
                for(int j = 0; j < this.GetRow(0).Length; j++)
                    Console.Write(this.matrix[i, j].ToString() + " ");
                Console.WriteLine();
            }
        }
    }
}
