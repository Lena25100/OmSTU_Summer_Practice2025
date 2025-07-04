using System.Reflection;

namespace task05
{
    public class ClassAnalyzer
    {
        private Type _type;

        public ClassAnalyzer(Type type)
        {
            _type = type;
        }

        public IEnumerable<string> GetPublicMethods()
        => _type
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Select(method => method.Name);

        public IEnumerable<string> GetMethodParams(string methodname)
        { 
            var method = _type.GetMethod(methodname, BindingFlags.Public | BindingFlags.Instance);
            if (method == null)
            { 
                return Enumerable.Empty<string>();
            }
            return method
                .GetParameters()
                .Select(parameter => parameter.Name)
                .Where(name => name != null)!;
        }

        public IEnumerable<string> GetAllFields()
        => _type
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Select(field => field.Name);

        public IEnumerable<string> GetProperties()
        => _type
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Select(property => property.Name);

        public bool HasAttribute<T>() where T : Attribute
        => _type
            .GetCustomAttributes(typeof(T))
            .Any();

    }
}
