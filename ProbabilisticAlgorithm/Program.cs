using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProbabilisticAlgorithm // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private const int N1 = 10000000, N2 = 100000000, N3 = 1000000000;

        public static void Main(string[] args)
        {
            for (int i = 0; i < 1; i++)
            {
                UseQueenSearch();
            }
            

        }

        static void UseDarts()
        {
            double pi1 = HitOrMiss.Darts(N1);
            Console.WriteLine($"N={N1}, Pi={pi1}");
            double pi2 = HitOrMiss.Darts(N2);
            Console.WriteLine($"N={N2}, Pi={pi2}");
            double pi3 = HitOrMiss.Darts(N3);
            Console.WriteLine($"N={N3}, Pi={pi3}");
        }
         
        static void UseDarts2()
        {
            double pi1 = HitOrMiss.Darts2(N1);
            Console.WriteLine($"N={N1}, Pi={pi1}");
            double pi2 = HitOrMiss.Darts2(N2);
            Console.WriteLine($"N={N2}, Pi={pi2}");
            double pi3 = HitOrMiss.Darts2(N3);
            Console.WriteLine($"N={N3}, Pi={pi3}");
        }

        static void UseMCCalculus()
        {
            var N = new int[] { 1000, 1000000, 100000000, 1000000000 };
            for(int i = 0; i < N.Length; i++)
            {
                Console.WriteLine($" N = {N[i]}, Pi = {HitOrMiss.MCCalculus(N[i])}");
            }
        }


        static void UseCalculusF()
        {
            var N = new int[] { 1000, 1000000, 100000000, 1000000000 };
            for(int i = 0; i < N.Length; i++)
            {
                Console.WriteLine($" N = {N[i]}, result = {HitOrMiss.CalculusF(0,3*Math.PI,-1,1,N[i],Math.Sin)}");
            }
        }

        static void UseSetCount()
        {
            var N = new int[] { 10, 100, 1000, 10000, 100000, 1000000, 10000000};
            int repeat = 50000;

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //for(int i = 0;i < N.Length; i++)
            //{
            //    var result = Math.Round(HitOrMiss.SetCount(N[i], repeat), 5);
            //    var lossRate = Math.Abs(1 - result / N[i]) * 100;
            //    Console.WriteLine($" n = {N[i]}, result = {result}, loss = {Math.Round(lossRate,2,MidpointRounding.AwayFromZero)}%");
            //}
            //sw.Stop();
            //TimeSpan ts1 = sw.Elapsed;
            //Console.WriteLine($"compute spent {ts1.TotalSeconds}s.");

            //sw.Restart();
            for (int i = 0;i < N.Length; i++)
            {
                var result = HitOrMiss.ParallelSetCount(N[i], repeat);
                var lossRate = Math.Abs(1 - result / N[i]) * 100;
                Console.WriteLine($" n = {N[i]}, result = {result}, loss = {Math.Round(lossRate,2,MidpointRounding.AwayFromZero)}%");
            }
            //sw.Stop();
            //TimeSpan ts2 = sw.Elapsed;
            //Console.WriteLine($"parallel compute spent {ts2.TotalSeconds}s.");

        }
        static void UseSearchOrderList()
        {
            var N = new int[] { 10000/*000, 100000, 100000000 */};
            int repeat = 1000;
            for(int i = 0; i < N.Length; i++)
            {
                SearchOrderList.Search(N[i], repeat);
            }
        }
        static void UseQueenSearch()
        {
            var N = Enumerable.Range(12, 9).ToArray();
            int repeat = 5000;
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < N.Length; i++)
            {
                StepVegas.SearchQueens1(N[i], repeat);
            }
            sw.Stop();
            Console.WriteLine($"spent {sw.Elapsed.TotalSeconds}s");
            sw.Restart();
            for (int i = 0; i < N.Length; i++)
            {
                StepVegas.SearchQueens(N[i], repeat);
            }
            sw.Stop();
            Console.WriteLine($"spent {sw.Elapsed.TotalSeconds}s");
        }
    }
}