using System.Collections.Concurrent;
using CommandLib;



namespace task18
{
    public class SchedulerThread
    {
        private readonly ConcurrentQueue<ICommand> _queue = new ConcurrentQueue<ICommand>();
        private readonly IScheduler? _scheduler;
        private bool _is_running = false;
        private Thread? _thread;
        private bool _softStop = false;

        public SchedulerThread(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public Thread? ThisThread => _thread;

        public void Start()
        {
            _thread = new Thread(Work);
            _is_running = true;
            _softStop = false;
            _thread.Start();
        }

        public void AddCommand(ICommand command) => _queue.Enqueue(command);

        public void SoftStop() => _softStop = true;
        public void HardStop() => _is_running = false;

        private void Work()
        {
            while (_is_running)
            {
                if (_queue.TryDequeue(out var command))
                {
                    command.Execute();

                    continue;
                }
                if (_scheduler!.HasCommand())
                {
                    var command_scheduler = _scheduler.Select();
                    command_scheduler?.Execute();

                    continue;
                }
                if (_softStop) _is_running = false;

                else Thread.Sleep(100);
            }
        }
    }
}
