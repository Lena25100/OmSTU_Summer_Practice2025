using CommandLib;

namespace task18
{
    public interface IScheduler
    {
        bool HasCommand();
        ICommand? Select();
        void AddCommand(ICommand command);
    }
}
