using MyCalculator.Interfaces;

namespace WpfApp;

public class MessageService : ICalculatorMessageService
{
    public void Show(string message)
    {
        var messageWindow = new MessageWindow(message);
        messageWindow.ShowDialog();
    }
}