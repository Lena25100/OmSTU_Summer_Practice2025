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
    }
}

