namespace MyCalculator.Interfaces;

public interface ICalculatorView
{
    /// <summary>
    /// �������, ������������� ��� ��������� �������������� �����-���������
    /// </summary>
    event Action MultButtonClicked;

    /// <summary>
    /// �������, ������������� ��� ��������� �������������� �����-���������
    /// </summary>
    event Action SumButtonClicked;

    /// <summary>
    /// �������, ������������� ��� ��������� �������������� �����-���������
    /// </summary>
    event Action SubButtonClicked;

    /// <summary>
    /// �������, ������������� ��� ��������� �������������� �����-���������
    /// </summary>
    event Action DivButtonClicked;

    /**
     * Displays the result of the calculation
     */
    void PrintResult(double result);

    /**
     * Shows an error, such as division by 0, empty arguments, etc.
     */
    void DisplayError(string message);

    /**
     * Returns the value entered in the first argument field
     */
    string GetFirstArgumentAsString();

    /**
     * Returns the value entered in the second argument field
     */
    string GetSecondArgumentAsString();
}