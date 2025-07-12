using task11;
using Xunit;

namespace task11tests
{
    public class CalculatorGeneratorTests
    {
        private readonly ICalculator _calculator;
        public CalculatorGeneratorTests()
        {
            _calculator = CalculatorGenerator.Generator();
        }

        [Fact]
        public void Add_ReturnsCorrectValue()
        {
            int result = _calculator.Add(13, 4);
            Assert.Equal(17, result);
        }

        [Fact]
        public void Minus_ReturnsCorrectValue()
        {
            int result = _calculator.Minus(13, 4);
            Assert.Equal(9, result);
        }

        [Fact]
        public void Mul_ReturnsCorrectValue()
        {
            int result = _calculator.Mul(10, 7);
            Assert.Equal(70, result);
        }

        [Fact]
        public void DivByZero_ThrowsDivideByZeroException()
        {
            Assert.Throws<DivideByZeroException>(() => _calculator.Div(70, 0));
        }

        [Fact]
        public void Div_ReturnsCorrectValue()
        {
            int result = _calculator.Div(70, 10);
            Assert.Equal(7, result);
        }
    }
}
