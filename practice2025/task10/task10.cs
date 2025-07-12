using System.Reflection;
using Plugin;

namespace task10
{
    public class PluginLoader
    {
        private readonly string path;
        public PluginLoader(string path)
        {
            this.path = path;
        }

        public void FindPluginsAndLoad()
        {
            var plugins = new List<(Type _class, string name, string[] PlaginsDependencies)>();

            foreach (var dll_path in Directory.GetFiles(path, "*.dll"))
            {
                var assembly = Assembly.LoadFrom(dll_path);

                foreach (var type in assembly.GetTypes())
                {
                    var attribute = type.GetCustomAttribute<PluginLoadAttribute>();
                    if (typeof(IPluginCommand).IsAssignableFrom(type) &&
                        !type.IsInterface &&
                        attribute != null)
                    {
                        plugins.Add((type, type.Name, attribute.PlaginsDependencies));
                    }
                }
            }

            var loadedPlugins = new HashSet<string>();
            while (loadedPlugins.Count < plugins.Count)
            {
                foreach (var plugin in plugins)
                {
                    if (!loadedPlugins.Contains(plugin.name) &&
                        plugin.PlaginsDependencies != null &&
                        plugin.PlaginsDependencies.All(loadedPlugins.Contains))
                    {
                        var instance = (IPluginCommand)Activator.CreateInstance(plugin._class)!;
                        instance.Execute();
                        loadedPlugins.Add(plugin.name);
                    }
                }


            }
        }
    }
}
