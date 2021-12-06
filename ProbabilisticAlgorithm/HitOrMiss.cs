using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbabilisticAlgorithm
{
     static class HitOrMiss
    {
        private static readonly object countLock = new object();
        public static double Darts(int n)
        {
            int k = 0;
            double x = 0, y = 0;
            var seed = Guid.NewGuid().GetHashCode();
            var rand = new Random(seed);
            for (int i = 0; i < n; i++)
            {
                x = rand.NextDouble();
                y = rand.NextDouble();
                if (x * x + y * y <= 1)
                {
                    k++;
                }
            }
            return 4.0 * k / n;
        }
        public static double Darts2(int n)
        {
            int k = 0;
            double x = 0, y = 0;
            var seed = Guid.NewGuid().GetHashCode();
            var rand = new Random(seed);
            for (int i = 0; i < n; i++)
            {
                x = rand.NextDouble();
                y = x;
                if (x * x + y * y <= 1)
                {
                    k++;
                }
            }
            return 4.0 * k / n;
        }

        private static double F(double x)
        {
            return Math.Sqrt(1 - (x * x));
        }
        /// <summary>
        /// Monte Carlo积分计算pi
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static double MCCalculus(int n)
        {
            var seed = Guid.NewGuid().GetHashCode();
            var rand = new Random(seed);
            int k = 0;
            double x = 0, y = 0;
            for (int i = 0; i < n; i++)
            {
                x=rand.NextDouble();
                y=rand.NextDouble();
                double fx = F(x);
                if (y < fx)
                {
                    k++;
                }
            }
            return 4.0 * k / n;
        }

        private static double Sin(double x)
        {
            return Math.Sin(x);
        }
        /// <summary>
        /// 求ff(x)在[a,b]的定积分
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <param name="n"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static double CalculusF(double a, double b, double c, double d, int n, Func<double,double> func)
        {
            var seed = Guid.NewGuid().GetHashCode();
            var rand = new Random(seed);
            int k = 0;
            double x = 0, y = 0, fx=0;
            double total = (b-a)*(d-c);
            for(int i = 0; i < n; i++)
            {
                x= rand.NextDouble();
                x = x * (b - a) + a;//x映射到定义域
                y= rand.NextDouble();
                y = y * (d - c) + c;//y映射到f(x)值域
                fx = func(x);
                if(0 < y && y < fx && fx>0)
                {
                    k++;
                }
                if (0 > y && y > fx && fx < 0)
                {
                    k--;
                }
            }
            return total * k / n;
        }

        /// <summary>
        /// 集合元素数量
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static decimal SetCount(int x, int n)
        {
            decimal result = 0;
            for (int i = 0; i < n; i++)
            {
                var tmp = InternalSetCount(x);
                //result += tmp;
                result = result * i / (i + 1) + tmp / (i + 1);
            }
            return result;
        }
        /// <summary>
        /// 并行估算集合元素数量
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static decimal ParallelSetCount(int x, int n)
        {
            Int64 result = 0;
            double count = 0;
            decimal cnt = 0;
            Parallel.For(0, n,
                index =>
                {
                    var tmp = InternalSetCount(x);
                    //Interlocked.Add(ref result, (Int64)tmp);
                    lock(countLock){
                        cnt += tmp;
                    }
                    
                });
            return cnt / n;
            //return 1.0*result/n;
        }

        public static decimal InternalSetCount(int x)
        {
            int[] arr=Enumerable.Range(1, x).ToArray();
            int k = 1;
            var seed = Guid.NewGuid().GetHashCode();
            var rand = new Random(seed);
            var tmp = new List<int>(arr[rand.Next(0,x)]);
            int min = x, max = 0;
            while (true)
            {
                int r = rand.Next(0,x);
                if(r < min)
                {
                    min = r;
                }
                if(r > max)
                {
                    max = r;
                }
                if (tmp.Contains(arr[r]))
                {
                    break;
                }
                else
                {
                    tmp.Add(arr[r]);
                    k++;
                }
            }
            return ((decimal)(2 * k * k / Math.PI));

        }
    }
}
