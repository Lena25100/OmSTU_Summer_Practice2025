using task18;
using CommandLib;
using Xunit;

public class TestCommand : ICommand
{
    public int ExecutedCount = 0;
    public void Execute() => Interlocked.Increment(ref ExecutedCount);
}

public class SchedulerThreadTests
{
    [Fact]
    public void SchedulerThread_Should_ExecuteQueuedCommands()
    {
        var scheduler = new RoundRobinScheduler();
        var serverThread = new SchedulerThread(scheduler);

        var first_command = new TestCommand();
        var second_command = new TestCommand();

        scheduler.AddCommand(first_command);
        scheduler.AddCommand(second_command);

        serverThread.Start();

        Thread.Sleep(500);

        serverThread.HardStop();
        serverThread.ThisThread?.Join(1000);

        Assert.True(first_command.ExecutedCount > 0);
        Assert.True(second_command.ExecutedCount > 0);
    }

    [Fact]
    public void ServerThread_Should_ExecuteCommands_AfterStart()
    {
        var scheduler = new RoundRobinScheduler();
        var serverThread = new SchedulerThread(scheduler);

        var command = new TestCommand();

        serverThread.Start();

        Thread.Sleep(50);

        scheduler.AddCommand(command);

        Thread.Sleep(300);

        serverThread.HardStop();
        serverThread.ThisThread?.Join(1000);

        Assert.True(command.ExecutedCount > 0);
    }

    [Fact]
    public void Scheduler_Should_Ignore_NullCommand()
    {
        var scheduler = new RoundRobinScheduler();

        scheduler.AddCommand(null!);

        Assert.False(scheduler.HasCommand());
    }

    [Fact]
    public void Scheduler_Should_ExecuteCommandsRoundRobin()
    {
        var scheduler = new RoundRobinScheduler();
        var serverThread = new SchedulerThread(scheduler);

        var first_command = new TestCommand();
        var second_command = new TestCommand();
        var third_command = new TestCommand();

        scheduler.AddCommand(first_command);
        scheduler.AddCommand(second_command);
        scheduler.AddCommand(third_command);

        serverThread.Start();

        Thread.Sleep(1000);

        serverThread.HardStop();
        serverThread.ThisThread?.Join(1000);

        Assert.True(first_command.ExecutedCount > 1);
        Assert.True(second_command.ExecutedCount > 1);
        Assert.True(third_command.ExecutedCount > 1);
    }
}
