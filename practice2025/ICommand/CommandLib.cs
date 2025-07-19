namespace CommandLib
{
    public interface ICommand
    {
        void Execute();
    }

    public class TestCommand(int id) : ICommand
    {
        int counter = 0;
        public int ExecutionCount => counter;

        public void Execute()
        {
            counter++;
            Console.WriteLine($"Поток {id} вызов {counter}");
        }
    }
}
