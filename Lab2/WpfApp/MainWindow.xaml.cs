using MyCalculator.Interfaces;
using System;
using System.Windows;

namespace WpfApp;

public partial class MainWindow : Window, ICalculatorView
{
    private readonly ICalculatorMessageService _messageService;

    public event Action? MultButtonClicked;
    public event Action? SumButtonClicked;
    public event Action? SubButtonClicked;
    public event Action? DivButtonClicked;

    public MainWindow(ICalculatorMessageService messageService)
    {
        _messageService = messageService;
        InitializeComponent();
    }

    public void DisplayError(string message)
    {
        _messageService.Show(message);
    }

    public string GetFirstArgumentAsString()
    {
        return FirstArg.Text;
    }

    public string GetSecondArgumentAsString()
    {
        return SecondArg.Text;
    }

    public void PrintResult(double result)
    {
        _messageService.Show(result.ToString());
    }

    private void Mult_btn_Click(object sender, RoutedEventArgs e)
    {
        MultButtonClicked?.Invoke();
    }

    private void Sub_btn_Click(object sender, RoutedEventArgs e)
    {
        SubButtonClicked?.Invoke();
    }

    private void Sum_btn_Click(object sender, RoutedEventArgs e)
    {
        SumButtonClicked?.Invoke();
    }

    private void Div_btn_Click(object sender, RoutedEventArgs e)
    {
        DivButtonClicked?.Invoke();
    }
}