using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbabilisticAlgorithm
{
     static class ComputePi
    {
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
    }
}
