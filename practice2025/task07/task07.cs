using System.Reflection;
namespace task07
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
    public class DisplayNameAttribute : Attribute
    {
        public string DisplayName { get; }

        public DisplayNameAttribute(string DisplayName)
        {
            this.DisplayName = DisplayName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class VersionAttribute : Attribute
    {
        public int Major { get; }
        public int Minor { get; }

        public VersionAttribute(int Major, int Minor)
        {
            this.Major = Major;
            this.Minor = Minor;
        }

    }

    [DisplayName("Пример класса")]
    [Version(1,0)]
    public class SampleClass
    {
        [DisplayName("Тестовый метод")]
        public void TestMethod() { }

        [DisplayName("Числовое свойство")]
        public int Number { get; set; }
    }

    public static class ReflectionHelper
    {
        public static void PrintTypeInfo(Type type)
        {
            var display_name = type.GetCustomAttribute<DisplayNameAttribute>();
            if (display_name != null) Console.WriteLine($"Отображаемое имя класса: {display_name.DisplayName}");

            var version_attribute = type.GetCustomAttribute<VersionAttribute>();
            if (version_attribute != null) Console.WriteLine($"Версия класса: {version_attribute.Major}.{version_attribute.Minor}");

            Console.WriteLine("\nСвойства:");
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var property_attribute = property.GetCustomAttribute<DisplayNameAttribute>();
                if (property_attribute != null) Console.WriteLine($"{property.Name}: {property_attribute.DisplayName}");
            }

            Console.WriteLine("\nМетоды:");
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var method_attribute = method.GetCustomAttribute<DisplayNameAttribute>();
                if (method_attribute != null) Console.WriteLine($"{method.Name}: {method_attribute.DisplayName}");
            }

        }

    }
}
