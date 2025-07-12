using Plugin;
namespace PluginsLib
{
    [PluginLoad]
    public class FirstPlugin : IPluginCommand
    {
        public void Execute()
        {
            Console.WriteLine("Первый плагин выполнился!");
        }
    }

    [PluginLoad("FirstPlugin")]
    public class SecondPlugin : IPluginCommand
    {
        public void Execute()
        {
            Console.WriteLine("Второй плагин выполнился!");
        }
    }

    [PluginLoad("SecondPlugin")]
    public class ThirdPlugin : IPluginCommand
    {
        public void Execute()
        {
            Console.WriteLine("Третий плагин выполнился!");
        }
    }
}

