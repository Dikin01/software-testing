using MyCalculator.Interfaces;

namespace MyCalculator.Models;

public class Calculator : ICalculator
{
    public const string DivideByZeroExceptionMessageFormatter =
        "|{0}| must be greater than {1:g2}";

    public double Sum(double a, double b) => a + b;

    public double Subtract(double a, double b) => a - b;

    public double Multiply(double a, double b) => a * b;

    public double Divide(double a, double b)
    {
        if (Math.Abs(b) < ICalculator.Epsilon)
            throw new DivideByZeroException(
                string.Format(DivideByZeroExceptionMessageFormatter, b, ICalculator.Epsilon));

        return a / b;
    }
}