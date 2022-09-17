using Calculator.Interfaces;

namespace Calculator;

public class Calculator : ICalculator
{
    public double Sum(double a, double b) => a + b;

    public double Subtract(double a, double b) => a - b;

    public double Multiply(double a, double b) => a * b;

    public double Divide(double a, double b)
    {
        if (Math.Abs(b) < 10e-8)
        {
            throw new ArithmeticException();
        }

        return a / b;
    }
}