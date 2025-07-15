/*
using System.Threading;

namespace task14
{
    public class DefiniteIntegral
    {
        public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
        {
            double total_result = 0.0;

            double length = (b - a) / threadsnumber;

            Barrier barrier = new Barrier(threadsnumber + 1);

            for (int i = 0; i < threadsnumber; i++)
            {
                double thread_start = a + i * length;
                double thread_end = (i == threadsnumber - 1) ? b : thread_start + length;

                Thread thread = new Thread(_ =>
                {

                    double total_sum = 0.0;
                    for (double a = thread_start; a < thread_end; a += step)
                    {
                        double b = Math.Min(a + step, thread_end);
                        total_sum += (function(a) + function(b)) / 2.0 * (b - a);
                    }

                    Interlocked.Exchange(ref total_result, total_result + total_sum);

                    barrier.SignalAndWait();
                });

                thread.Start();
            }

            barrier.SignalAndWait();
            barrier.Dispose();



            return total_result;
        }
        public static double SolveSingleThread(double a, double b, Func<double, double> function, double step)
        {
            double result = 0.0;

            for (double x = a; x < b; x += step)
            {
                double next = Math.Min(x + step, b);
                result += (function(x) + function(next)) / 2.0 * (next - x);
            }

            return result;
        }
    }
}*/
using System.Threading;

namespace task14
{
    public static class DefiniteIntegral
    {
        public static double Solve(double a, double b, Func<double, double> function, double step, int threadsnumber)
        {
            if (threadsnumber <= 1)
            {
                return SolveSingleThread(a, b, function, step);
            }

            double length = (b - a) / threadsnumber;
            int n = (int)(length / step);
            int chunkSize = n / threadsnumber;
            double result = 0;
            object locker = new object();

            Parallel.For(0, threadsnumber, i =>
            {
                double thread_start = a + i * length;
                double thread_end = (i == threadsnumber - 1) ? b : thread_start + length;

                double total_sum = 0.0;
                for (double a = thread_start; a < thread_end; a += step)
                {
                    double b = Math.Min(a + step, thread_end);
                    total_sum += (function(a) + function(b)) / 2.0 * (b - a);
                }

                lock (locker)
                {
                    result += total_sum;
                }
            });

            return result;
        }
        public static double SolveSingleThread(double a, double b, Func<double, double> function, double step)
        {
            double sum_res = 0;
            int n = (int)((b - a) / step);
            for (int i = 0; i < n; i++)
            {
                double start = a + i * step;
                double end = start + step;
                double sum = (function(start) + function(end)) * step / 2.0;
                sum_res += sum;
            }

            return sum_res;
        }
    }
}
