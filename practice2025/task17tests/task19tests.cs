using CommandLib;
using task18;
using Xunit;

public class SchedulerThreadTests
{
    [Fact]
    public void Test_Commands_Execute3Times()
    {
        var scheduler = new RoundRobinScheduler();
        var thread = new SchedulerThread(scheduler);

        var commands = new List<TestCommand>();

        for (int i = 0; i < 5; i++)
        {
            var command = new TestCommand(i);
            commands.Add(command);
            scheduler.AddCommand(command);
        }

        thread.Start();

        Thread.Sleep(2000);

        thread.HardStop();
        thread.ThisThread?.Join(1000);

        foreach (var command in commands)
        {
            Assert.True(command.ExecutionCount >= 3,
                $"Команда {command} выполнилась {command.ExecutionCount} раз, ожидалось >= 3");
        }
    }
}
