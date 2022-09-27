using MyCalculator.Interfaces;

namespace MyCalculator.Views;

public class CalculatorView : ICalculatorView
{
    private readonly StreamReader _input;
    private readonly StreamWriter _output;
    
    private string? _firstArgument;
    private string? _secondArgument;

    public event Action? MultButtonClicked;
    public event Action? SumButtonClicked;
    public event Action? SubButtonClicked;
    public event Action? DivButtonClicked;

    public CalculatorView(Stream stream)
    {
        _input = new StreamReader(stream);
        _output = new StreamWriter(stream);
    }
    
    public void PrintResult(double result)
    {
        _output.WriteLine($"Result: {result}");
        _output.Flush();
    }

    public void DisplayError(string message)
    {
        _output.WriteLine($"Error: {message}");
        _output.Flush();
    }

    public string GetFirstArgumentAsString()
    {
        _firstArgument ??= _input.ReadLine()!;
        if (_firstArgument is null)
            throw new InvalidOperationException("Stream doesn't contain first argument");
        return _firstArgument;
    }

    public string GetSecondArgumentAsString()
    {
        GetFirstArgumentAsString();
        _secondArgument ??= _input.ReadLine()!;
        if (_secondArgument is null)
            throw new InvalidOperationException("Stream doesn't contain second argument");
        return _secondArgument;
    }
}