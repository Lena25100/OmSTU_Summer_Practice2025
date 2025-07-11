using Plugin;
using task10;
using Xunit;

namespace task10tests
{
    [PluginLoad]
    public class FirstPlugin : IPluginCommand
    {
        public void Execute()
        {
            Console.WriteLine("������ ������ ����������!");
        }
    }

    [PluginLoad("FirstPlugin")]
    public class SecondPlugin : IPluginCommand
    {
        public void Execute()
        {
            Console.WriteLine("������ ������ ����������!");
        }
    }

    [PluginLoad("SecondPlugin")]
    public class ThirdPlugin : IPluginCommand
    {
        public void Execute()
        {
            Console.WriteLine("������ ������ ����������!");
        }
    }

    public class PluginLoadTests : IDisposable
    {
        private readonly string PluginDirectory;

        public PluginLoadTests()
        {
            PluginDirectory = Path.Combine(Directory.GetCurrentDirectory(), "TestPlugins");

            if (Directory.Exists(PluginDirectory)) Directory.Delete(PluginDirectory, true);
            Directory.CreateDirectory(PluginDirectory);

            var pluginAssemblyPath = typeof(FirstPlugin).Assembly.Location;
            File.Copy(pluginAssemblyPath, Path.Combine(PluginDirectory, "MyPlugins.dll"));
        }

        public void Dispose()
        {
            if (Directory.Exists(PluginDirectory)) Directory.Delete(PluginDirectory, true);
        }


        [Fact]
        public void PluginLoaderExecuteAllPlugins()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var plugin_loader = new PluginLoader(PluginDirectory);
            plugin_loader.FindPluginsAndLoad();

            var consOutput = output.ToString();
            Assert.Contains("������ ������ ����������!", consOutput);
            Assert.Contains("������ ������ ����������!", consOutput);
            Assert.Contains("������ ������ ����������!", consOutput);
        }

        [Fact]
        public void PluginLoaderExecutePluginsInCorrectOrder()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var plugin_loader = new PluginLoader(PluginDirectory);
            plugin_loader.FindPluginsAndLoad();

            var consOutput = output.ToString();
            int index_1 = consOutput.IndexOf("������ ������ ����������!");
            int index_2 = consOutput.IndexOf("������ ������ ����������!");
            int index_3 = consOutput.IndexOf("������ ������ ����������!");

            Assert.True(index_1 >= 0);
            Assert.True(index_2 >= 0);
            Assert.True(index_3 >= 0);
            Assert.True(index_2 > index_1);
            Assert.True(index_3 > index_2);
        }
    }
}