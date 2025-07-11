using System.Reflection;

namespace task09
{
    class task09
    {
        static void PrintTypeInfo(Assembly assembly)
        {
            foreach (var _class in assembly.GetTypes())
            {
                Console.WriteLine($"Класс: {_class.FullName}");

                PrintAttributes(_class);
                PrintConstructors(_class);
                PrintMethods(_class);

                Console.WriteLine();
            }
        }

        static void PrintAttributes(Type type)
        {
            foreach (var attribute in type.GetCustomAttributes())
            {
                Console.WriteLine($"Атрибут класса: {attribute.GetType().Name}");
            }
        }

        static void PrintConstructors(Type type)
        {
            foreach (var constructor in type.GetConstructors())
            {
                Console.WriteLine("Конструктор: ");

                foreach (var parameter in constructor.GetParameters())
                {
                    Console.WriteLine($"Имя и тип параметра: {parameter.Name} - {parameter.ParameterType.Name}");
                }
            }
        }

        static void PrintMethods(Type type)
        {
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                Console.WriteLine($"Метод: {method.Name}");

                foreach (var parameter in method.GetParameters())
                {
                    Console.WriteLine($"Имя и тип параметра: {parameter.Name} - {parameter.ParameterType.Name}");
                }
            }
        }

        static void Main(string[] path)
        {
            if (path.Length == 0 || !File.Exists(path[0]))
            {
                Console.WriteLine("Укажите верный путь к .dll");
            }

            Assembly assembly = Assembly.LoadFrom(path[0]);

            PrintTypeInfo(assembly);
        }
    }
}

