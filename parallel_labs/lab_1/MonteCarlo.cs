using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace lab_1
{
    internal class MonteCarlo
    {
        static int GetHitNum(int dotsNum)
        {
            int sum = 0;
            for (int i = 0; i < dotsNum; i++)
            {
                Random rand = new Random();
                double x = rand.NextDouble();
                double y = rand.NextDouble();
                if (x * x + y * y < 1.0) sum++;
            }

            return sum;
        }
        
        public static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int dotsPerTask, tasksNumber;
            dotsPerTask = int.Parse(Console.ReadLine());
            tasksNumber = int.Parse(Console.ReadLine());
            double N = 0.0;

            Task<int>[] tasks = new Task<int>[tasksNumber];

            for (int i = 0; i < tasksNumber; i++)
            {
                tasks[i] = new Task<int>(x => GetHitNum((int)x), dotsPerTask);
                tasks[i].Start();
            }

            for (int i = 0; i < tasksNumber; i++)
            {
                N += tasks[i].Result;
            }
    
            sw.Stop();
            Console.WriteLine("Pi number = " + (double)N*4/(dotsPerTask*tasksNumber) + "\n time passed = " + sw.ElapsedMilliseconds + "ms");
            Console.ReadKey();
        }
    }
}