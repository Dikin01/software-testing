using System.Windows;

namespace WpfApp;

public partial class MessageWindow : Window
{
    public MessageWindow(string message)
    {
        InitializeComponent();
        MessageText.Content = message;
    }
}