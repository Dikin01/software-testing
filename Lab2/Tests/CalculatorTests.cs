using MyCalculator.Interfaces;
using MyCalculator.Models;

namespace Tests;

public class CalculatorTests
{
    private readonly ICalculator _calculator = new Calculator();
    
    # region Sum
    
    [Fact]
    public void Sum_ShouldReturn5_When1And1()
    {
        var result = _calculator.Sum(1, 1);
        
        result.Should().Be(2);
    }
    
    [Fact]
    public void Sum_ShouldReturn0_When1AndMinus1()
    {
        var result = _calculator.Sum(1, -1);
        
        result.Should().Be(0);
    }
    
    [Fact]
    public void Sum_ShouldReturn0_WhenMinus1And1()
    {
        var result = _calculator.Sum(-1, 1);

        result.Should().Be(0);
    }
    
    [Fact]
    public void Sum_ShouldReturnMinus2_WhenMinus1AndMinus1()
    {
        var result = _calculator.Sum(-1, -1);

        result.Should().Be(-2);
    }
    
    # endregion
    
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
    
    # region Divide when result > 1
    
    [Fact]
    public void TestDivideFunctionWithPositiveNumbersWhenResultMoreThenOne()
    {
        var result = _calculator.Divide(10, 3);
        
        Assert.Equal(3.3333, result, 4);
    }
    
    [Fact]
    public void TestDivideFunctionWithPositiveAndNegativeNumbersWhenResultMoreThenOne()
    {
        var result = _calculator.Divide(10, -4);
        
        Assert.Equal(-2.5, result, 4);
    }
    
    [Fact]
    public void TestDivideFunctionWithNegativeAndPositiveNumbersWhenResultMoreThenOne()
    {
        var result = _calculator.Divide(-12, 5);
        
        Assert.Equal(-2.4, result, 4);
    }
    
    [Fact]
    public void TestDivideFunctionWithNegativeNumbersWhenResultMoreThenOne()
    {
        var result = _calculator.Divide(-12, -7);
        
        Assert.Equal(1.71428571, result, 4);
    }
    
    # endregion
    
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

    #region Divide throw exception

    [Fact]
    public void TestDivideFunctionWhenSecondArgumentTooSmall()
    {
        var act = () => _calculator.Divide(5, 0.0000000001);
        
        act.Should().Throw<DivideByZeroException>();
    }
    
    [Fact]
    public void TestDivideFunctionWhenBothArgumentsTooSmall()
    {
        var act = () => _calculator.Divide(0.0000000002, 0.0000000001);

        act.Should().Throw<DivideByZeroException>();
    }
    
    #endregion
}