using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace task11
{
    public interface ICalculator
    {
        public int Add(int a, int b);
        public int Minus(int a, int b);
        public int Mul(int a, int b);
        public int Div(int a, int b);
    }

    public class CalculatorGenerator
    {
        public static ICalculator Generator()
        {
            string code = @"using task11;
            public class Calculator : ICalculator
            {
                public int Add(int a, int b) => a + b;
                public int Minus(int a, int b) => a - b;
                public int Mul(int a, int b) => a * b;
                public int Div(int a, int b) 
                {
                    if (b == 0)
                        throw new System.DivideByZeroException(""На ноль делить нельзя."");
                    return a / b;   
                }
            }";

            MetadataReference[] refs = new MetadataReference[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location)
            };

            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

            Compilation compilation = CSharpCompilation.Create(
                "Assembly",
                new[] { tree },
                refs,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            MemoryStream memory_stream = new MemoryStream();
            compilation.Emit(memory_stream);

            Assembly assembly = Assembly.Load(memory_stream.ToArray());

            Type? calculatorType = assembly.GetType("Calculator");

            if (calculatorType == null)
                throw new ArgumentNullException("Calculator не найден!");

            object? instance = Activator.CreateInstance(calculatorType)!;
            ICalculator calc = (ICalculator)instance!;

            if (instance == null)
                throw new Exception("Создать класс не получилось!");

            return calc;

        }
    }
}


