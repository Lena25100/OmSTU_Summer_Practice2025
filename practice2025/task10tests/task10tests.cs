using Plugin;
using PluginsLib;
using task10;
using Xunit;

namespace task10tests
{

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
            Assert.Contains("Первый плагин выполнился!", consOutput);
            Assert.Contains("Второй плагин выполнился!", consOutput);
            Assert.Contains("Третий плагин выполнился!", consOutput);
        }

        [Fact]
        public void PluginLoaderExecutePluginsInCorrectOrder()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            var plugin_loader = new PluginLoader(PluginDirectory);
            plugin_loader.FindPluginsAndLoad();

            var consOutput = output.ToString();
            int index_1 = consOutput.IndexOf("Первый плагин выполнился!");
            int index_2 = consOutput.IndexOf("Второй плагин выполнился!");
            int index_3 = consOutput.IndexOf("Третий плагин выполнился!");

            Assert.True(index_1 >= 0);
            Assert.True(index_2 >= 0);
            Assert.True(index_3 >= 0);
            Assert.True(index_2 > index_1);
            Assert.True(index_3 > index_2);
        }
    }
}
