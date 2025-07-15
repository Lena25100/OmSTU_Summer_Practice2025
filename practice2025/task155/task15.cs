using ScottPlot;
using System.Diagnostics;
using task14;

namespace task15
{
    class task15
    {
        static void Main()
        {
            Func<double, double> function_sin = Math.Sin;
            double[] steps = new double[]{ 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6 };
            double min_step = 0;

            foreach (double step in steps)
            {
                double result = DefiniteIntegral.SolveSingleThread(-100, 100, function_sin, step);
                if (Math.Abs(result) <= 1e-4)
                {
                    min_step = step;
                    break;
                }
            }

            int[] threads = { 1, 2, 4, 8, 16 };
            int repeats = 100;

            double single_thread_time = 0;
            for (int i = 0; i < repeats; i++)
            {
                Stopwatch sw = Stopwatch.StartNew();
                double res = DefiniteIntegral.SolveSingleThread(-100, 100, function_sin, min_step);
                sw.Stop();
                single_thread_time += sw.Elapsed.TotalMilliseconds;
            }
            single_thread_time /= repeats;

            var averageTimesList = new List<double>();

            foreach (int thread in threads)
            {
                double time_sum = 0;

                for (int i = 0; i < repeats; i++)
                {
                    var sw = Stopwatch.StartNew();
                    double result = DefiniteIntegral.Solve(-100, 100, function_sin, min_step, thread);
                    sw.Stop();
                    
                    time_sum += sw.Elapsed.TotalMilliseconds;
                }

                var average_time = time_sum / repeats;
                averageTimesList.Add(average_time);
            }

            double min_time = double.MaxValue;
            int total_threads_count = 1;
            double[] avgTimes = averageTimesList.ToArray();

            for (int i = 1; i < avgTimes.Length; i++)
            {
                if (avgTimes[i] < min_time)
                {
                    min_time = avgTimes[i];
                    total_threads_count = threads[i];
                }
            }

            double diff = ((single_thread_time - min_time) / single_thread_time) * 100;

            var plot = new Plot();
            plot.Add.Scatter(avgTimes, threads.Select(thread => (double)thread).ToArray());

            string plot_path = @"C:\Users\Елена\plot.png";
            string txt_path = @"C:\Users\Елена\result.txt";

            plot.XLabel("Время (мс)");
            plot.YLabel("Количество потоков");
            plot.Title("Время работы функции Solve");
            plot.SavePng(plot_path , 600, 400);

            string file = $"Шаг: {min_step}\n" +
                $"Время выполнения однопоточной реализации: {Math.Round(single_thread_time, 2)} мс\n" +
                $"Оптимальное количество потоков: {total_threads_count}\n" +
                $"Лучшее время выполнения многопоточной реализации: {Math.Round(min_time, 2)} мс\n" +
                $"Разница (в процентах): {Math.Round(diff, 2)} %\n";

            File.WriteAllText(txt_path, file);
        }
    }
}
