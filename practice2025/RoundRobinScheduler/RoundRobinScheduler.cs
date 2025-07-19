using System.Collections.Concurrent;
using CommandLib;
using task18;

namespace task18
{
    public class RoundRobinScheduler : IScheduler
    {
        private readonly ConcurrentQueue<ICommand> _commands = new ConcurrentQueue<ICommand>();

        public bool HasCommand() => !_commands.IsEmpty;
        public ICommand? Select()
        {
            if (_commands.TryDequeue(out var command))
            {
                _commands.Enqueue(command);
                return command;
            }

            return null;
        }

        public void AddCommand(ICommand command)
        {
            if (command != null)
                _commands.Enqueue(command);
        }
    }
}
