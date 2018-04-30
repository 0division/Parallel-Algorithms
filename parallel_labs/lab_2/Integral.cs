using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace lab_2
{
    internal static class Integral //sqrt(x^2)+1
    {
        static double TrapMethod(List<double> argList)
        {
            return ((Math.Sqrt(Math.Pow(argList[0],2)) + Math.Sqrt(Math.Pow(argList[1],2) + 2)) * argList[2]/ 2);
        }
        
        public static void Main(string[] args)
        {
            double a = double.Parse(Console.ReadLine()); //from 
            double b = double.Parse(Console.ReadLine()); //to
            int n = int.Parse(Console.ReadLine()); //number of points
            
            double h = (b - a) / n; //step
            
            double[] x = new double[n]; //points
            for (int i = 0; i < n; i++)
            {
                x[i] = a + (i * h);
            }
            
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            Task<double>[] tasks = new Task<double>[n];
            //double result = 0.0;
            for (int i = 0; i < n-1; i++)
            {
                List<double> ls = new List<double> {x[i], x[i+1], h};

                tasks[i] = new Task<double>(z => TrapMethod((List<double>)z),ls);
                tasks[i].Start();

                //result += TrapMethod(ls);
            }

            double res = 0.0;
            for (int i = 0; i < n-1; i++)
            {
                res += tasks[i].Result;
            }
            sw.Stop();

            
            Console.WriteLine("integral sqrt(x^2)+1 = "+res+"\ntime passed = "+sw.ElapsedMilliseconds+"ms");
            Console.ReadKey();
        }
    }
}