using MyCalculator.Interfaces;
using System.Globalization;

namespace MyCalculator.Presenters;

public class CalculatorPresenter : ICalculatorPresenter
{
    public const string FirstArgErrorMessage = "Parse first argument error";
    public const string SecondArgErrorMessage = "Second first argument error";

    private readonly ICalculator _calculator;
    private readonly ICalculatorView _calculatorView;

    public CalculatorPresenter(ICalculator calculator, ICalculatorView calculatorView)
    {
        _calculator = calculator;
        _calculatorView = calculatorView;
        _calculatorView.MultButtonClicked += OnMultiplyClicked;
        _calculatorView.DivButtonClicked += OnDivideClicked;
        _calculatorView.SubButtonClicked += OnMinusClicked;
        _calculatorView.SumButtonClicked += OnPlusClicked;
    }

    public void OnPlusClicked()
    {
        var parseSuccess = TryParseArgumentsAndHandleError(out var first, out var second);

        if (!parseSuccess)
            return;

        var result = _calculator.Sum(first, second);
        _calculatorView.PrintResult(result);
    }

    public void OnMinusClicked()
    {
        var parseSuccess = TryParseArgumentsAndHandleError(out var first, out var second);

        if (!parseSuccess)
            return;

        var result = _calculator.Subtract(first, second);
        _calculatorView.PrintResult(result);
    }

    public void OnDivideClicked()
    {
        var parseSuccess = TryParseArgumentsAndHandleError(out var first, out var second);

        if (!parseSuccess)
            return;

        try
        {
            var result = _calculator.Divide(first, second);
            _calculatorView.PrintResult(result);
        }
        catch (DivideByZeroException e)
        {
            _calculatorView.DisplayError(e.Message);
        }
    }

    public void OnMultiplyClicked()
    {
        var parseSuccess = TryParseArgumentsAndHandleError(out var first, out var second);

        if (!parseSuccess)
            return;

        var result = _calculator.Multiply(first, second);
        _calculatorView.PrintResult(result);
    }

    private bool TryParseArgumentsAndHandleError(out double first, out double second)
    {
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        var parseSuccess = double.TryParse(_calculatorView.GetFirstArgumentAsString(), out first);
        if (!parseSuccess)
            _calculatorView.DisplayError(FirstArgErrorMessage);

        parseSuccess = double.TryParse(_calculatorView.GetSecondArgumentAsString(), out second);
        if (!parseSuccess)
            _calculatorView.DisplayError(SecondArgErrorMessage);

        return parseSuccess;
    }
}