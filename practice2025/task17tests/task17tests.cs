using Xunit;
using task17;
using CommandLib;

namespace task17tests
{

    public class task17tests
    {
        public class ServerThreadTests
        {
            public class TestCommand : ICommand
            {
                public void Execute() { }
            }

            private bool IsThreadWorking(Thread? _thread) => _thread != null && _thread.IsAlive;

            [Fact]
            public void ServerThread_SoftStopExecuteAllCommandsBeforeStop()
            {
                var serverThread = new ServerThread();

                serverThread.Start();

                serverThread.AddCommand(new TestCommand());
                serverThread.AddCommand(new TestCommand());
                serverThread.AddCommand(new SoftStopCommand(serverThread));

                var thisThread = serverThread.ThisThread;

                thisThread?.Join(1000);

                Assert.False(IsThreadWorking(thisThread));
            }

            [Fact]
            public void HardStopCommand_Should_StopThread_Immediately()
            {
                var serverThread = new ServerThread();

                serverThread.Start();

                Thread.Sleep(300);

                serverThread.AddCommand(new TestCommand());
                serverThread.AddCommand(new TestCommand());
                serverThread.AddCommand(new HardStopCommand(serverThread));

                var thisThread = serverThread.ThisThread;

                thisThread?.Join(1000);

                Assert.False(IsThreadWorking(thisThread));
            }

            [Fact]
            public void ServerThread_ThrowsExceptionIfSoftStopNotInServerThread()
            {
                var serverThread = new ServerThread();

                serverThread.Start();

                var softStop = new SoftStopCommand(serverThread);

                var expected = Assert.Throws<InvalidOperationException>(() => softStop.Execute());

                Assert.Contains("Ошибка! SoftStop должна выполняться только в том потоке команд, который она останавливает!", expected.Message);

                serverThread.HardStop();

                var thisThread = serverThread.ThisThread;

                thisThread?.Join(1000);
            }

            [Fact]
            public void ServerThread_ThrowsExceptionIfHardStopNotInServerThread()
            {
                var serverThread = new ServerThread();

                serverThread.Start();

                var hardStop = new HardStopCommand(serverThread);

                var expected = Assert.Throws<InvalidOperationException>(() => hardStop.Execute());

                Assert.Contains("Ошибка! HardStop должна выполняться только в том потоке команд, который она останавливает!", expected.Message);

                serverThread.HardStop();

                var thisThread = serverThread.ThisThread;

                thisThread?.Join(1000);
            }
        }
    }
}
