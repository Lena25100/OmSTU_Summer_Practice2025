namespace Plugin
{
    public interface IPluginCommand
    {
        void Execute();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PluginLoadAttribute : Attribute
    {
        public string[] PlaginsDependencies { get; private set; }

        public PluginLoadAttribute(params string[] plagins_dependencies)
        {
            PlaginsDependencies = plagins_dependencies;
        }
    }
}
