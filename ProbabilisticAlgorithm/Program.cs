using System;
using System.Collections.Generic;
using System.Linq;

namespace ProbabilisticAlgorithm // Note: actual namespace depends on the project name.
{
    public class Program
    {
        private const int N1 = 10000000, N2 = 100000000, N3 = 1000000000;

        public static void Main(string[] args)
        {
            for (int i = 0; i < 2; i++)
            {
                UseSetCount();
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
            var N = new int[] { 100, 1000000, 100000000};
            for(int i = 0;i < N.Length; i++)
            {
                double result=HitOrMiss.SetCount(N[i]);
                double lossRate=Math.Abs(1-result/N[i])*100;
                Console.WriteLine($" n = {N[i]}, result = {result}, loss = {Math.Round(lossRate,2,MidpointRounding.AwayFromZero)}%");
            }
        }
    }
}