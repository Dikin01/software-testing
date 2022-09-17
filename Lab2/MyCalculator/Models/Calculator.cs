using MyCalculator.Interfaces;

namespace MyCalculator.Models;

public class Calculator : ICalculator
{
    public double Sum(double a, double b) => a + b;

    public double Subtract(double a, double b) => a - b;

    public double Multiply(double a, double b) => a * b;

    public double Divide(double a, double b)
    {
        if (Math.Abs(b) < 10e-8)
        {
            throw new DivideByZeroException($"|{nameof(b)}| must be greater than 10e-8");
        }

        return a / b;
    }
}