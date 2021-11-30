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
                UseMCCalculus();
            }
            

        }

        static void UseDarts()
        {
            double pi1 = ComputePi.Darts(N1);
            Console.WriteLine($"N={N1}, Pi={pi1}");
            double pi2 = ComputePi.Darts(N2);
            Console.WriteLine($"N={N2}, Pi={pi2}");
            double pi3 = ComputePi.Darts(N3);
            Console.WriteLine($"N={N3}, Pi={pi3}");
        }

        static void UseDarts2()
        {
            double pi1 = ComputePi.Darts2(N1);
            Console.WriteLine($"N={N1}, Pi={pi1}");
            double pi2 = ComputePi.Darts2(N2);
            Console.WriteLine($"N={N2}, Pi={pi2}");
            double pi3 = ComputePi.Darts2(N3);
            Console.WriteLine($"N={N3}, Pi={pi3}");
        }

        static void UseMCCalculus()
        {
            var N = new int[] { 1000, 1000000, 100000000, 1000000000 };
            for(int i = 0; i < N.Length; i++)
            {
                Console.WriteLine($" N = {N[i]}, Pi = {ComputePi.MCCalculus(N[i])}");
            }
        }
    }
}