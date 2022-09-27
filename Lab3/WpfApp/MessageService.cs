using MyCalculator.Interfaces;
using System.Windows;

namespace WpfApp;

public class MessageService : ICalculatorMessageService
{
    public void Show(string message)
    {
        MessageBox.Show(message);
    }
}
