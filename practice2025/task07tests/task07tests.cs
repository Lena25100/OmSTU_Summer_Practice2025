using Xunit;
using System.Reflection;
using task07;

namespace task07tests
{
    public class AttributeReflectionTests
    {
        [Fact]
        public void Class_HasDisplayNameAttribute()
        {
            var type = typeof(SampleClass);
            var attribute = type.GetCustomAttribute<DisplayNameAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal("Пример класса", attribute.DisplayName);
        }

        [Fact]
        public void Method_HasDisplayNameAttribute()
        {
            var method = typeof(SampleClass).GetMethod("TestMethod");
            var attribute = method.GetCustomAttribute<DisplayNameAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal("Тестовый метод", attribute.DisplayName);
        }

        [Fact]
        public void Property_HasDisplayNameAttribute()
        {
            var prop = typeof(SampleClass).GetProperty("Number");
            var attribute = prop.GetCustomAttribute<DisplayNameAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal("Числовое свойство", attribute.DisplayName);
        }

        [Fact]
        public void Class_HasVersionAttribute()
        {
            var type = typeof(SampleClass);
            var attribute = type.GetCustomAttribute<VersionAttribute>();
            Assert.NotNull(attribute);
            Assert.Equal(1, attribute.Major);
            Assert.Equal(0, attribute.Minor);
        }

        [Fact]
        public void PrintTypeInfo_OutputsCorrectly()
        {
            var type = typeof(SampleClass);
            var output = new StringWriter();
            Console.SetOut(output);

            ReflectionHelper.PrintTypeInfo(type);
            var outPut = output.ToString();
            Assert.Contains("Отображаемое имя класса: Пример класса", outPut);
            Assert.Contains("Версия класса: 1.0", outPut); 
            Assert.Contains("Тестовый метод", outPut);
            Assert.Contains("Числовое свойство", outPut);
        }

    }
}
