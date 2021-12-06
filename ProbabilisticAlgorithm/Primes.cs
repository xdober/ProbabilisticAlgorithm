using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbabilisticAlgorithm
{
    internal static class Primes
    {
        public static Random random = new();

        private static List<int> _primes = new();
        private static List<bool> _primeOrNot = new();
        public static List<bool> PrimeOrNot
        {
            get 
            {
                if (_primeOrNot.Count() == 0)
                {
                    _primeOrNot = Enumerable.Repeat(true, 10001).ToList();
                    _primeOrNot[1] = false;
                    Stopwatch sw = new();
                    sw.Start();
                    for (int i = 2; i < 101; i++)
                    {
                        for (int j = 2 * i; j < 10001; j += i)
                        {
                            _primeOrNot[j] = false;
                        }
                    }
                    sw.Stop();
                    //Console.WriteLine($" 筛法耗时{sw.Elapsed.TotalMilliseconds} ms");
                }
                return _primeOrNot;
            }
        }
        public static List<int> RealPrimes
        {
            get
            {
                if (_primes.Count() == 0)
                {
                    for(int i = 2; i < 10001; i++)
                    {
                        if (PrimeOrNot[i])
                        {
                            _primes.Add(i);
                        }
                    }
                }
                return _primes;
            }
        }
        public static List<int> ComputePrimes()
        {
            var maybeOrNot = Enumerable.Repeat(false, 10001).ToList();
            maybeOrNot[2] = true;
            maybeOrNot[3] = true;
            var sw = new Stopwatch();
            sw.Start();
            for(int i = 5; i < 10001; i += 2)
            {
                if (RepeatMillRab(i, /*(int)Math.Log10(i)*/10))
                {
                    maybeOrNot[i] = true;
                }
            }
            sw.Stop();
            //Console.WriteLine($" RepeatMillRab use {sw.Elapsed.TotalMilliseconds} ms");
            //比较错误率
            List<int> incs = new();
            for(int i = 101; i < 10001; i+=2)
            {
                if (maybeOrNot[i] != PrimeOrNot[i])
                {
                    incs.Add(i);
                }
            }
            //Console.WriteLine($" incorrect {incs.Count()} of {RealPrimes.Count(x => x > 100)} primes from 100 to 10000");
            //if (incorrect != 0)
            //{
            //    Console.Write($" The incorrect(s) is(are) {String.Join(", ",incs)}\n");
            //}
            return incs;
        }
        public static bool RepeatMillRab(int n, int k)
        {
            for(int i = 1; i <= k; i++)
            {
                if (!MillRab(n))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool MillRab(int n)
        {
            int a = random.Next(2, n-1);

            return Btest(a,n);
        }
        /// <summary>
        /// n为奇数，2<=a<=n-2
        /// </summary>
        /// <param name="a"></param>
        /// <param name="n"></param>
        /// <returns> true 当且仅当 a属于B(n)，即n是强伪素数或素数</returns>
        public static bool Btest(int a, int n)
        {
            int s = 0, t = n - 1;
            while (t % 2 == 0)//n-1=(2^s)*t
            {
                s++;
                t /= 2;
            }
            long x = a;
            for(int i = 1; i < t; i++)
            {
                x = (x * a) % n;
            }
            //x = ((long)Math.Pow(a,t)) % n;
            if(x==1 || x==n-1)
            {
                return true;
            }
            for(int i = 1; i <= s; i++)
            {
                x = x * x % n;
                if (x == n - 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
