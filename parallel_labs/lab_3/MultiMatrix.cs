using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace lab_3
{
    class MultiMatrix
    {
        static double[,] MultiplyP(double[,] a, double[,] b) //В два с небольшим раза быстрее обычного умножения
        {
            double[,] res = new double[a.GetLength(1), b.GetLength(0)];

            Parallel.For(0, a.GetLength(1), i =>
                {
                    for (int j = 0; j < b.GetLength(0); j++)
                    {
                        for (int k = 0; k < a.GetLength(0); k++)
                        {
                            res[i, j] += a[i, k] * b[k, j];
                        }
                    }
                }
            );

            return res;
        }
        
        static double[,] Multiply(double[,] a, double[,] b) //Обычное умножение
        {
            double[,] res = new double[a.GetLength(1), b.GetLength(0)];

            for(int i =0; i < a.GetLength(1); i++)
                {
                    for (int j = 0; j < b.GetLength(0); j++)
                    {
                        for (int k = 0; k < a.GetLength(0); k++)
                        {
                            res[i, j] += a[i, k] * b[k, j];
                        }
                    }
                }

            return res;
        }

        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
            string[] lines = File.ReadAllLines("matrix.csv");
            double[,] matrix = new double[lines.Length, lines.Length];
            int i = 0;
            foreach (string line in lines)
            {
                string[] elements = line.Split(',');
                int j = 0;
                foreach (string str in elements)
                {
                    matrix[i, j] = Double.Parse(str, CultureInfo.InvariantCulture);
                    j++;
                }
                i++;
            }
            
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            double[,] MultipliedMatrix = MultiplyP(matrix, matrix);
            
            sw.Stop();
            
            Console.WriteLine("Multiply complete, "+sw.ElapsedMilliseconds+"ms");
            
            string[] stringArr = new string[lines.Length];

            for (int j = 0; j < lines.Length; j++)
            {
                string matrValue = "";
                for (int k = 0; k < lines.Length; k++)
                {
                    matrValue += "," + MultipliedMatrix[j, k].ToString();
                }

                stringArr[j] = matrValue;
            }

            File.WriteAllLines("colomn.csv", stringArr);

            Console.WriteLine("done");
        }
    }
}