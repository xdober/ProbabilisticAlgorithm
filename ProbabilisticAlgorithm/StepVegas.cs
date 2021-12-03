using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProbabilisticAlgorithm
{
    public static class StepVegas
    {
        public static void SearchQueens(int n, int repeat)
        {
            var lv = new LasVegas(n);
            Int64 now, min=Int64.MaxValue, bestk=0 ;
            for(int k = 0; k < n; k++)
            {
                now = 0;

                Parallel.For(0, repeat, 
                    i => {
                        var lvi = new LasVegas(n);
                        var current = lvi.SearchQueens(n, k);
                        Interlocked.Add(ref now, current);
                });
                if( now < min)
                {
                    bestk = k;
                    min = now;
                }
            }
            Console.WriteLine($"n = {n} bestK={bestk} nodes = {1.0*min / repeat,5}");
        }
        public static void SearchQueens1(int n, int repeat)
        {
            var lv = new LasVegas(n);
            Int64 now, min = Int64.MaxValue, bestk = 0;
            for (int k = 0; k < n; k++)
            {
                now = 0;

                for(var i = 0; i < repeat; i++)
                {
                    var lvi = new LasVegas(n);
                    now += lvi.SearchQueens(n, k);
                }

                if (now < min)
                {
                    bestk = k;
                    min = now;
                }
            }
            Console.WriteLine($"n = {n} bestK={bestk} nodes = {1.0 * min / repeat,5}");
        }
    }

    public class LasVegas
    {
        private List<int> tries;
        private List<bool> col;
        private List<bool> diag45;
        private List<bool> diag135;
        public Int64 try_count;
        private readonly int N;
        private readonly Random rand;

        public LasVegas(int n)
        {
            try_count = 0;
            tries = Enumerable.Repeat(0,n).ToList();
            diag45 = Enumerable.Repeat(false, 2 * n).ToList(); 
            diag135 = Enumerable.Repeat(false, 2 * n).ToList();
            col = Enumerable.Repeat(false, n).ToList();
            N = n;
            rand = new Random(Guid.NewGuid().GetHashCode());
        }

        public bool QueensLv(int n, int k)
        {
            int i, row;
            col = Enumerable.Repeat(false, n).ToList();
            diag45 = Enumerable.Repeat(false, 2 * n).ToList();
            diag135 = Enumerable.Repeat(false, 2 * n).ToList();
            var canSelect = new List<int>();
            for(row = 0; row < k; row++)
            {
                canSelect.Clear();
                int open = 0;
                int selected = 0;//选择的列号
                for (i = 0; i < n; i++)//每一列
                {
                    if(!col[i] && !diag45[n+i-row] && !diag135[i + row])
                    {
                        open++;
                        canSelect.Add(i);
                    }
                }
                if(open == 0)
                {
                    return false;
                }
                else
                {
                    selected = canSelect[rand.Next(0,open)];
                }
                try_count++;
                tries[row] = selected;
                col[selected] = true;
                diag45[n+selected-row] = true;
                diag135[selected+row] = true;
            }
            return true;
        }
        public bool Backtrack(int n, int row)
        {
            int i;
            if(row == n)
            {
                return true;
            }
            for(i = 0; i < n; ++i)
            {
                if (!col[i] && !diag45[n+i-row] && !diag135[i + row])
                {
                    try_count++;
                    tries[row] = i;
                    col[i] = diag45[n + i - row] = diag135[i + row] = true;
                    if(Backtrack(n, row+1))
                    {
                        return true;
                    }
                    col[i] = diag45[n+i-row] = diag135[i + row] = false;
                }
            }
            return false;
        }

        public void ResetParams()
        {
            try_count = 0;
            tries = Enumerable.Repeat(0, N).ToList();
            diag45 = Enumerable.Repeat(false, 2 * N).ToList();
            diag135 = Enumerable.Repeat(false, 2 * N).ToList();
            col = Enumerable.Repeat(false, N).ToList();
        }
        public Int64 SearchQueens(int n, int k)
        {
            while (true)
            {
                if(QueensLv(n,k))
                {
                    if(Backtrack(n,k))
                    {
                        return try_count;
                    }
                }
            }
        }
    }

}
