namespace Tests;

public class CalculatorTests
{
    # region Sum function
    
    [Fact]
    public void TestSumFunctionWithPositiveNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Sum(2, 3);

        // Assert
        Assert.Equal(5, result);
    }
    
    [Fact]
    public void TestSumFunctionWithPositiveAndNegativeNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Sum(2, -5);

        // Assert
        Assert.Equal(-3, result);
    }
    
    [Fact]
    public void TestSumFunctionWithNegativeAndPositiveNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Sum(-3, 4);

        // Assert
        Assert.Equal(1, result);
    }
    
    [Fact]
    public void TestSumFunctionWithNegativeNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Sum(-5, -10);

        // Assert
        Assert.Equal(-15, result);
    }
    
    # endregion
    
    # region Subtract function
    
    [Fact]
    public void TestSubtractFunctionWithPositiveNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Subtract(10, 3);

        // Assert
        Assert.Equal(7, result);
    }
    
    [Fact]
    public void TestSubtractFunctionWithPositiveAndNegativeNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Subtract(10, -5);

        // Assert
        Assert.Equal(15, result);
    }
    
    [Fact]
    public void TestSubtractFunctionWithNegativeAndPositiveNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Subtract(-10, 5);

        // Assert
        Assert.Equal(-15, result);
    }
    
    [Fact]
    public void TestSubtractFunctionWithNegativeNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Subtract(-2, -3);

        // Assert
        Assert.Equal(1, result);
    }
    
    # endregion
    
    # region Multiply function
    
    [Fact]
    public void TestMultiplyFunctionWithPositiveNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Multiply(5, 11);

        // Assert
        Assert.Equal(55, result);
    }
    
    [Fact]
    public void TestMultiplyFunctionWithPositiveAndNegativeNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Multiply(7, -8);

        // Assert
        Assert.Equal(-56, result);
    }
    
    [Fact]
    public void TestMultiplyFunctionWithNegativeAndPositiveNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Multiply(-6, 9);

        // Assert
        Assert.Equal(-54, result);
    }
    
    [Fact]
    public void TestMultiplyFunctionWithNegativeNumbers()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Multiply(-3, -4);

        // Assert
        Assert.Equal(12, result);
    }
    
    # endregion
    
    # region Divide function when result > 1
    
    [Fact]
    public void TestDivideFunctionWithPositiveNumbersWhenResultMoreThenOne()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Divide(10, 3);

        // Assert
        Assert.Equal(3.3333, result, 4);
    }
    
    [Fact]
    public void TestDivideFunctionWithPositiveAndNegativeNumbersWhenResultMoreThenOne()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Divide(10, -4);

        // Assert
        Assert.Equal(-2.5, result, 4);
    }
    
    [Fact]
    public void TestDivideFunctionWithNegativeAndPositiveNumbersWhenResultMoreThenOne()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Divide(-12, 5);

        // Assert
        Assert.Equal(-2.4, result, 4);
    }
    
    [Fact]
    public void TestDivideFunctionWithNegativeNumbersWhenResultMoreThenOne()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Divide(-12, -7);

        // Assert
        Assert.Equal(1.71428571, result, 4);
    }
    
    # endregion
    
    # region Divide function when result < 1
    
    [Fact]
    public void TestDivideFunctionWithPositiveNumbersWhenResultLessThenOne()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Divide(3, 7);

        // Assert
        Assert.Equal(0.42857143, result, 4);
    }
    
    [Fact]
    public void TestDivideFunctionWithPositiveAndNegativeNumbersWhenResultLessThenOne()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Divide(4, -9);

        // Assert
        Assert.Equal(-0.44444444, result, 4);
    }
    
    [Fact]
    public void TestDivideFunctionWithNegativeAndPositiveNumbersWhenResultLessThenOne()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Divide(-1, 6);

        // Assert
        Assert.Equal(-0.16666667, result, 4);
    }
    
    [Fact]
    public void TestDivideFunctionWithNegativeNumbersWhenResultLessThenOne()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act
        var result = calculator.Divide(-6, -11);

        // Assert
        Assert.Equal(0.54545455, result, 4);
    }
    
    # endregion

    #region Divide function throw exception

    [Fact]
    public void TestDivideFunctionWhenSecondArgumentTooSmall()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act & Assert
        Assert.Throws<ArithmeticException>(() => { calculator.Divide(5, 0.0000000001); });
    }
    
    [Fact]
    public void TestDivideFunctionWhenBothArgumentsTooSmall()
    {
        // Arrange
        var calculator = new Calculator.Calculator();

        // Act & Assert
        Assert.Throws<ArithmeticException>(() => { calculator.Divide(0.0000000002, 0.0000000001); });
    }
    
    #endregion
    
}