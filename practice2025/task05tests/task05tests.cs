using Xunit;
using task05;

namespace task05tests
{
    public class TestClass
    {
        public int PublicField;
        private string _privateField;
        public int Property1 { get; set; }
        private int Property2 { get; set; }

        public void Method() { }
        public void MethodWithParams(int parameter1, string parameter2) { }
    }

    [Serializable]
    public class AttributedClass { }

    public class ClassAnalyzerTests
    {
        [Fact]
        public void GetPublicMethods_ReturnsCorrectMethods()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var methods = analyzer.GetPublicMethods();

            Assert.Contains("Method", methods);
        }

        [Fact]
        public void GetMethodParams_ReturnsCorrectMethodParameters()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var parameters = analyzer.GetMethodParams("MethodWithParams");

            Assert.Contains("parameter1", parameters);
            Assert.Contains("parameter2", parameters);
        }

        [Fact]
        public void GetAllFields_IncludesPrivateAndPublicFields()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var fields = analyzer.GetAllFields();

            Assert.Contains("_privateField", fields);
            Assert.Contains("PublicField", fields);
        }

        [Fact]
        public void GetProperties_ReturnsCorrectProperties()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var properties = analyzer.GetProperties();

            Assert.Contains("Property1", properties);
            Assert.Contains("Property2", properties);
        }

        [Fact]
        public void HasAttribute_ReturnsTrueIfAttributeExists()
        {
            var analyzer = new ClassAnalyzer(typeof(AttributedClass));
            var attribute = analyzer.HasAttribute<SerializableAttribute>();

            Assert.True(attribute);
        }

        [Fact]
        public void HasAttribute_ReturnsFalseIfAttributeDoesNotExist()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var attribute = analyzer.HasAttribute<SerializableAttribute>();
            Assert.False(attribute);
        }

    }
}
