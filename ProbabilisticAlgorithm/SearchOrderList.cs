using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbabilisticAlgorithm
{
    static class SearchOrderList
    {
        public static void Search(int n, int repeat)
        {
            var searchInstance = new SearchList(n);
            int x;
            decimal[] counts = new decimal[4];
            int[] maxCount = Enumerable.Repeat(0, 4).ToArray();
            string[] algorithmName = { "A(x)", "B(x)", "C(x)", "D(x)", };
            for(int i = 0; i < repeat; i++)
            {
                x = searchInstance.rand.Next(0, n) * 2;
                var retA = searchInstance.SearchA(x);
                counts[0] += retA.Item2;
                if(maxCount[0] < retA.Item2)
                {
                    maxCount[0] = retA.Item2;
                }
                var retB = searchInstance.SearchB(x);
                counts[1] += retB.Item2;
                if(maxCount[1] < retB.Item2)
                {
                    maxCount[1] = retB.Item2;
                }
                var retC = searchInstance.SearchC(x);
                counts[2] += retC.Item2;
                if(maxCount[2] < retC.Item2)
                {
                    maxCount[2] = retC.Item2;
                }
                var retD = searchInstance.SearchD(x);
                counts[3] += retD.Item2;
                if(maxCount[3] < retD.Item2)
                {
                    maxCount[3] = retD.Item2;
                }
            }

            for(int i = 0; i < 4; i++)
            {
                Console.WriteLine($" N ={n} {algorithmName[i]}: average {counts[i]/repeat}, max {maxCount[i]}");
            }

            //var sw = new Stopwatch();
            //sw.Start();
            //var result = searchInstance.SearchA(x);
            //sw.Stop();
            //Console.WriteLine($" find {x} at pos {result} of {n}, A(x) spent {sw.Elapsed.TotalMilliseconds}");
            //sw.Restart();
            //result = searchInstance.SearchB(x);
            //sw.Stop();
            //Console.WriteLine($" find {x} at pos {result} of {n}, B(x) spent {sw.Elapsed.TotalMilliseconds}");
            //sw.Restart();
            //result = searchInstance.SearchC(x);
            //sw.Stop();
            //Console.WriteLine($" find {x} at pos {result} of {n}, C(x) spent {sw.Elapsed.TotalMilliseconds}");
            //sw.Restart();
            //result = searchInstance.SearchD(x);
            //sw.Stop();
            //Console.WriteLine($" find {x} at pos {result} of {n}, D(x) spent {sw.Elapsed.TotalMilliseconds}");
        }
    }
          
    public class SearchList
    {
        private int[] val;
        private int[] ptr;
        private int[] revptr;
        private int[] rank; 
        private int head;
        private int len;
        public Random rand;
        public SearchList(int n)
        {
            val = Enumerable.Range(0, n).ToArray();
            Array.ForEach(val, (i) => { val[i] *= 2; });
            ptr = Enumerable.Range(1, n).ToArray();
            ptr[n-1] = 0;
            revptr = Enumerable.Range(-1, n).ToArray();
            
            rank = new int[n];
            Array.ConstrainedCopy(val, 0, rank, 0, n);
            head = 0;
            len = n;

            rand = new Random(Guid.NewGuid().GetHashCode());
            //Shuffle();

            //for(int i = 0; i < n; i++)
            //{
            //    Console.Write($"{i,3}");
            //}
            //Console.WriteLine();
            //for(int i = 0; i < n; i++)
            //{
            //    Console.Write($"{val[i],3}");
            //}
            //Console.WriteLine();
            //for(int i = 0; i < n; i++)
            //{
            //    Console.Write($"{ptr[i],3}");
            //}
            //Console.WriteLine();
            //for(int i = 0; i < n; i++)
            //{
            //    Console.Write($"{revptr[i],3}");
            //}
            //Console.WriteLine();

        }
        /// <summary>
        /// 将链表打乱顺序，并保证ptr的正确性
        /// </summary>
        private void Shuffle()
        {
            int tmpi,tmpVal,tmpPtr,tmpRev;
            for(int i = 0; i < len; i++)
            {
                tmpi = rand.Next(i,val.Length);

                if (tmpi == i)
                {
                    continue;
                }
                //swap val[i] and val[tmpi]
                tmpVal = val[i];
                val[i] = val[tmpi];
                val[tmpi] = tmpVal;

                // i与i后面交换
                if(tmpi == ptr[i])
                {

                    if (revptr[i] >= 0)
                    {
                        ptr[revptr[i]] = tmpi;
                    }
                    if (ptr[tmpi]!=0)
                    {
                        revptr[ptr[tmpi]] = i;
                    }
                    tmpPtr = ptr[tmpi];
                    ptr[tmpi] = i;
                    ptr[i] = tmpPtr;

                    revptr[tmpi] = revptr[i];
                    revptr[i] = tmpi;


                }
                // i与i前面交换
                else if (tmpi == revptr[i])
                {
                    ptr[tmpi] = ptr[i];
                    ptr[i] = tmpi;

                    revptr[i] = revptr[tmpi];
                    revptr[tmpi] = i;

                    revptr[ptr[tmpi]] = tmpi;
                    if(revptr[i] >= 0)
                    {
                        ptr[revptr[i]] = i;
                    }
                    
                }
                else
                {
                    //swap ptr
                    tmpPtr = ptr[tmpi];
                    ptr[tmpi] = ptr[i];
                    ptr[i] = tmpPtr;

                    //swap revPtr
                    tmpRev = revptr[i];
                    revptr[i] = revptr[tmpi];
                    revptr[tmpi] = tmpRev;
                    if (revptr[tmpi] >= 0)
                    {
                        ptr[revptr[tmpi]] = tmpi;
                    }
                    if (revptr[i] >= 0)
                    {
                        ptr[revptr[i]] = i;
                    }
                    revptr[ptr[tmpi]] = tmpi;
                    revptr[ptr[i]] = i;

                }

                if(revptr[i] == -1)
                {
                    head = i;
                }
                else if (revptr[tmpi] == -1)
                {
                    head = tmpi;
                }

                 

            }
            Console.WriteLine($" head = {head}");
        }
        private Tuple<int, int> Search(int x, int i)
        {
            int k = 0;
            while(x > val[i])
            {
                i = ptr[i];
                k++;
            }
            return new Tuple<int, int>(i, k);
        }

        /// <summary>
        /// algorithm A
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Tuple<int, int> SearchA (int x)
        {
            return Search(x, head);
        }

        /// <summary>
        /// algorithm D
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Tuple<int, int> SearchD(int x)
        {
            int i = rand.Next(0, len);
            int y = val[i];
            if (x > y)
            {
                return Search(x,ptr[i]);
            }
            else if (x < y)
            {
                return Search(x, head);
            }
            else
            {
                return new Tuple<int, int>(i,0);
            }
        }

        /// <summary>
        /// algorithm B
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Tuple<int, int> SearchB(int x)
        {
            int i = head;
            int max=val[i];
            int sqrtN=(int)Math.Sqrt(len);
            for(int j = 0; j < sqrtN; j++)
            {
                int y = val[j];
                if (y > max && y <= x)
                {
                    max = y;
                    i = j;
                }
            }
            var result = Search(x, i);
            return new Tuple<int, int>(result.Item1, result.Item2 + sqrtN);
        }
        /// <summary>
        /// algorithm C
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Tuple<int, int> SearchC(int x)
        {
            int id = head;
            int max = val[id];
            int sqrtN=(int)Math.Sqrt(len);
            int[] al=new int[sqrtN];
            int dis = len / sqrtN;
            for(int j = 0;j < sqrtN-1; j++)
            {
                al[j]=rand.Next(j*dis,(j+1)*dis);
            }
            al[sqrtN - 1] = rand.Next(len - dis, len);
            for(int i = 0; i < sqrtN; i++)
            {
                int y = val[al[i]];
                if(y > max && y <= x)
                {
                    max = y;
                    id = al[i];
                }
            } 
            var result = Search(x, id);
            return new Tuple<int, int>(result.Item1,result.Item2+sqrtN);
        }
    }
}
