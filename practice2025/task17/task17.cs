using System.Collections.Concurrent;
using CommandLib;

namespace task17
{
    public class HardStopCommand : ICommand
    {
        public ServerThread _thread;

        public HardStopCommand(ServerThread thread)
        {
            _thread = thread;
        }

        public void Execute()
        {
            if (Thread.CurrentThread == _thread.ThisThread)
            {
                _thread.HardStop();
            }
            else
            {
                throw new InvalidOperationException("Ошибка! HardStop должна выполняться только в том потоке команд, который она останавливает!");
            }

        }
    }


    public class SoftStopCommand : ICommand
    {
        private readonly ServerThread _thread;

        public SoftStopCommand(ServerThread thread)
        {
            _thread = thread;
        }

        public void Execute()
        {
            if (Thread.CurrentThread == _thread.ThisThread)
            {
                _thread.SoftStop();
            }
            else
            {
                throw new InvalidOperationException("Ошибка! SoftStop должна выполняться только в том потоке команд, который она останавливает!");
            }
        }
    }

    public class ServerThread
    {
        private readonly ConcurrentQueue<ICommand> _queue = new ConcurrentQueue<ICommand>();
        private bool _is_running = false;
        private bool _softStop = false;
        private Thread? _thread;

        public Thread? ThisThread => _thread;

        public void Start()
        {
            _thread = new Thread(Work);
            _is_running = true;
            _softStop = false;
            _thread.Start();
        }

        public void AddCommand(ICommand command) => _queue.Enqueue(command);

        public void HardStop() => _is_running = false;

        public void SoftStop() => _softStop = true;

        private void Work()
        {
            while (_is_running)
            {
                if (!_queue.TryDequeue(out var command))
                {

                    if (_softStop)
                    {
                        _is_running = false;
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }

                    continue;
                }

                command.Execute();
            }
        }

    }
 
}
