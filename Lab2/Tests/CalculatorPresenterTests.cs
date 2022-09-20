using System.Globalization;
using MyCalculator.Interfaces;
using MyCalculator.Presenters;

namespace Tests;

public class CalculatorPresenterTests
{
    private readonly ICalculatorPresenter _presenter;

    private readonly Mock<ICalculator> _calculatorMock = new();
    private readonly Mock<ICalculatorView> _calculatorViewMock = new();

    private const double Result = 1;
    private const double FirstArgument = 2;
    private const double SecondArgument = 3;

    public CalculatorPresenterTests()
    {
        _calculatorViewMock.Setup(view => view.GetFirstArgumentAsString())
            .Returns(FirstArgument.ToString(CultureInfo.InvariantCulture));
        _calculatorViewMock.Setup(view => view.GetSecondArgumentAsString())
            .Returns(SecondArgument.ToString(CultureInfo.InvariantCulture));

        _calculatorMock.Setup(calculator => calculator.Sum(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(Result);
        _calculatorMock.Setup(calculator => calculator.Subtract(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(Result);
        _calculatorMock.Setup(calculator => calculator.Multiply(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(Result);
        _calculatorMock.Setup(calculator => calculator.Divide(It.IsAny<double>(), It.IsAny<double>()))
            .Returns(Result);

        _presenter = new CalculatorPresenter(_calculatorMock.Object, _calculatorViewMock.Object);
    }
    
    [Theory]
    [InlineData("", "a")]
    [InlineData(" ", "123B45")]
    public void OnPlusClicked_ShouldDisplayError_WhenArgumentsCanNotBeParsed(string first, string second)
    {
        _calculatorViewMock.Setup(view => view.GetFirstArgumentAsString()).Returns(first);
        _calculatorViewMock.Setup(view => view.GetSecondArgumentAsString()).Returns(second);
        
        _presenter.OnPlusClicked();
        _calculatorViewMock.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Exactly(2));
    }
    
    [Fact]
    public void OnPlusClicked_ShouldSumAndPrintResult_WhenArgumentsСanBeParsed()
    {
        _presenter.OnPlusClicked();

        _calculatorMock.Verify(calculator => calculator
            .Sum(FirstArgument, SecondArgument), Times.Once());
        _calculatorViewMock.Verify(view => view.PrintResult(Result), Times.Once);
    }
    
    [Theory]
    [InlineData("", "a")]
    [InlineData(" ", "123B45")]
    public void OnMinusClicked_ShouldDisplayError_WhenArgumentsCanNotBeParsed(string first, string second)
    {
        _calculatorViewMock.Setup(view => view.GetFirstArgumentAsString()).Returns(first);
        _calculatorViewMock.Setup(view => view.GetSecondArgumentAsString()).Returns(second);
        
        _presenter.OnMinusClicked();
        _calculatorViewMock.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Exactly(2));
    }

    [Fact]
    public void OnMinusClicked_ShouldSubtractAndPrintResult_WhenArgumentsСanBeParsed()
    {
        _presenter.OnMinusClicked();

        _calculatorMock.Verify(calculator => calculator
            .Subtract(FirstArgument, SecondArgument), Times.Once());
        _calculatorViewMock.Verify(view => view.PrintResult(Result), Times.Once);
    }
    
    [Theory]
    [InlineData("", "a")]
    [InlineData(" ", "123B45")]
    public void OnDivideClicked_ShouldDisplayError_WhenArgumentsCanNotBeParsed(string first, string second)
    {
        _calculatorViewMock.Setup(view => view.GetFirstArgumentAsString()).Returns(first);
        _calculatorViewMock.Setup(view => view.GetSecondArgumentAsString()).Returns(second);
        
        _presenter.OnDivideClicked();
        _calculatorViewMock.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Exactly(2));
    }

    [Fact]
    public void OnDivideClicked_ShouldDivideAndPrintResult_WhenArgumentsСanBeParsed()
    {
        _presenter.OnDivideClicked();

        _calculatorMock.Verify(calculator => calculator.Divide(FirstArgument, SecondArgument));
        _calculatorViewMock.Verify(view => view.PrintResult(Result), Times.Once);
    }

    [Fact]
    public void OnDivideClicked_ShouldDivideAndDisplayError_WhenDivideByZeroExceptionWasThrown()
    {
        _calculatorMock.Setup(calculator => calculator.Divide(It.IsAny<double>(), It.IsAny<double>()))
            .Throws(new DivideByZeroException());

        _presenter.OnDivideClicked();

        _calculatorMock.Verify(calculator => calculator.Divide(FirstArgument, SecondArgument), Times.Once);
        _calculatorViewMock.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Once);
    }
    
    [Theory]
    [InlineData("", "a")]
    [InlineData(" ", "123B45")]
    public void OnMultiplyClicked_ShouldDisplayError_WhenArgumentsCanNotBeParsed(string first, string second)
    {
        _calculatorViewMock.Setup(view => view.GetFirstArgumentAsString()).Returns(first);
        _calculatorViewMock.Setup(view => view.GetSecondArgumentAsString()).Returns(second);
        
        _presenter.OnMultiplyClicked();
        _calculatorViewMock.Verify(view => view.DisplayError(It.IsAny<string>()), Times.Exactly(2));
    }

    [Fact]
    public void OnMultiplyClicked_ShouldMultiplyAndPrintResult_WhenArgumentsСanBeParsed()
    {
        _presenter.OnMultiplyClicked();

        _calculatorMock.Verify(calculator => calculator
            .Multiply(FirstArgument, SecondArgument), Times.Once());
        _calculatorViewMock.Verify(view => view.PrintResult(Result), Times.Once);
    }
}