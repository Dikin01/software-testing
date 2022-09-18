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

    # region Subtract

    [Fact]
    public void TestSubtractFunctionWithPositiveNumbers()
    {
        var result = _calculator.Subtract(10, 3);

        Assert.Equal(7, result);
    }

    [Fact]
    public void TestSubtractFunctionWithPositiveAndNegativeNumbers()
    {
        var result = _calculator.Subtract(10, -5);

        Assert.Equal(15, result);
    }

    [Fact]
    public void TestSubtractFunctionWithNegativeAndPositiveNumbers()
    {
        var result = _calculator.Subtract(-10, 5);

        Assert.Equal(-15, result);
    }

    [Fact]
    public void TestSubtractFunctionWithNegativeNumbers()
    {
        var result = _calculator.Subtract(-2, -3);

        Assert.Equal(1, result);
    }

    # endregion

    # region Multiply

    [Fact]
    public void TestMultiplyFunctionWithPositiveNumbers()
    {
        var result = _calculator.Multiply(5, 11);

        Assert.Equal(55, result);
    }

    [Fact]
    public void TestMultiplyFunctionWithPositiveAndNegativeNumbers()
    {
        var result = _calculator.Multiply(7, -8);

        Assert.Equal(-56, result);
    }

    [Fact]
    public void TestMultiplyFunctionWithNegativeAndPositiveNumbers()
    {
        var result = _calculator.Multiply(-6, 9);

        Assert.Equal(-54, result);
    }

    [Fact]
    public void TestMultiplyFunctionWithNegativeNumbers()
    {
        var result = _calculator.Multiply(-3, -4);

        Assert.Equal(12, result);
    }

    # endregion

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

    # region Divide when result < 1

    [Fact]
    public void TestDivideFunctionWithPositiveNumbersWhenResultLessThenOne()
    {
        var result = _calculator.Divide(3, 7);

        Assert.Equal(0.42857143, result, 4);
    }

    [Fact]
    public void TestDivideFunctionWithPositiveAndNegativeNumbersWhenResultLessThenOne()
    {
        var result = _calculator.Divide(4, -9);

        Assert.Equal(-0.44444444, result, 4);
    }

    [Fact]
    public void TestDivideFunctionWithNegativeAndPositiveNumbersWhenResultLessThenOne()
    {
        var result = _calculator.Divide(-1, 6);

        Assert.Equal(-0.16666667, result, 4);
    }

    [Fact]
    public void TestDivideFunctionWithNegativeNumbersWhenResultLessThenOne()
    {
        var result = _calculator.Divide(-6, -11);

        Assert.Equal(0.54545455, result, 4);
    }

    # endregion

    [Fact]
    public void Divide_ShouldThrowDivideByZeroException_WhenDivisorIsLessThanEpsilon()
    {
        const double divisible = 1;
        const double divisor = ICalculator.Epsilon / 2;
        var act = () => _calculator.Divide(divisible, divisor);

        act.Should().Throw<DivideByZeroException>();
    }

    [Fact]
    public void TestDivideFunctionWhenBothArgumentsTooSmall()
    {
        var act = () => _calculator.Divide(0.0000000002, 0.0000000001);

        act.Should().Throw<DivideByZeroException>();
    }
}