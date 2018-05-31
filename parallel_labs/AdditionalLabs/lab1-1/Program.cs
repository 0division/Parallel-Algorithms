using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace lab1_1
{
    internal static class Integral //sqrt(x^2)+1
    {
        static double TrapMethod(List<double> argList)
        {
            return ((Math.Sqrt(Math.Pow(argList[0],2)) + Math.Sqrt(Math.Pow(argList[1],2) + 2))/2.0 * argList[2]);
        }
        
        public static void Main(string[] args)
        {
            var a = double.Parse(Console.ReadLine()); //from 
            Console.ReadKey();
            var b = double.Parse(Console.ReadLine()); //to
            Console.ReadKey();
            var n = int.Parse(Console.ReadLine()); //number of points
            
            var h = (b - a) / n; //step
            
            var x = new double[n+1];
            
            for (int i = 0; i < n+1; i++)
            {
                x[i] = a + (i * h);
            }
            
            var sw = new Stopwatch();
            sw.Start();
            
            var tasks = new Task<double>[n];
            var res = 0.0;
            
            for (int i = 0; i < n; i++)
            {
                var ls = new List<double> {x[i+1], x[i], h};

                tasks[i] = new Task<double>(z => TrapMethod((List<double>)z),ls);
                tasks[i].Start();
            }

            for (int i = 0; i < n-1; i++)
            {
                res += tasks[i].Result;
            }
            sw.Stop();
            
            Console.WriteLine("[Tasks] integral sqrt(x^2)+1 = "+res+"\ntime passed = "+sw.ElapsedMilliseconds+"ms");
            
            sw.Restart();

            var result = new double[n];
            Parallel.For( 0, n, i =>
            {
                var ls = new List<double> {x[i+1], x[i], h}; 
                result[i] = TrapMethod(ls);
            });
            res = result.Sum();
            sw.Stop();
            
            Console.WriteLine("[ParallelFor] integral sqrt(x^2)+1 = "+res+"\ntime passed = "+sw.ElapsedMilliseconds+"ms");
            
            Console.ReadKey();
        }
    }
}