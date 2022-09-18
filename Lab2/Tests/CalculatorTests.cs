using MyCalculator.Interfaces;
using MyCalculator.Models;

namespace Tests;

public class CalculatorTests
{
    private readonly ICalculator _calculator = new Calculator();

    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(1, -1, 0)]
    [InlineData(-1, 1, 0)]
    [InlineData(-1, -1, -2)]
    public void Sum_ShouldReturnExpectedNumber(double a, double b, double expected)
    {
        var result = _calculator.Sum(a, b);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2, 1, 1)]
    [InlineData(1, -1, 2)]
    [InlineData(-1, 1, -2 )]
    [InlineData(-1, -1, 0)]
    public void Subtract_ShouldReturnExpectedNumber(double a, double b, double expected)
    {
        var result = _calculator.Subtract(a, b);

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2, 1, 2)]
    [InlineData(1, -1, -1)]
    [InlineData(-2, 1, -2 )]
    [InlineData(-1, -2, 2)]
    public void Multiply_ShouldReturnExpectedNumber(double a, double b, double expected)
    {
        var result = _calculator.Multiply(a, b);

        result.Should().Be(expected);
    }
    

    [Theory]
    [InlineData(10, 3, 3.3333333333333335)]
    [InlineData(10, -4, -2.5)]
    [InlineData(-12, 5, -2.4)]
    [InlineData(-12, -7, 1.7142857142857142)]
    public void Divide_ShouldReturnExpectedNumber_WhenDivisibleIsGreaterThanDivisor(
        double divisible, double divisor, double expected)
    {
        var result = _calculator.Divide(divisible, divisor);

        result.Should().Be(expected);
    }
    
    [Theory]
    [InlineData(3, 7, 0.42857142857142855)]
    [InlineData(4, -9, -0.4444444444444444)]
    [InlineData(-1, 6, -0.16666666666666666)]
    [InlineData(-6, -11, 0.5454545454545454)]
    public void Divide_ShouldReturnExpectedNumber_WhenDivisibleIsSmallerThanDivisor(
        double divisible, double divisor, double expected)
    {
        var result = _calculator.Divide(divisible, divisor);

        result.Should().Be(expected);
    }
    
    [Fact]
    public void Divide_ShouldThrowDivideByZeroException_WhenDivisorIsLessThanEpsilon()
    {
        const double divisible = 1;
        const double divisor = ICalculator.Epsilon / 2;
        var act = () => _calculator.Divide(divisible, divisor);

        act.Should().Throw<DivideByZeroException>();
    }
}