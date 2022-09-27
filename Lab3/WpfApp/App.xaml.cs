using MyCalculator.Interfaces;
using MyCalculator.Models;
using MyCalculator.Presenters;
using System.Windows;

namespace WpfApp;

public partial class App : Application
{
    private readonly Window _window;

    private readonly ICalculator _calculator;
    private readonly ICalculatorView _view;
    private readonly ICalculatorPresenter _presenter;

    public App()
    {
        var message = new MessageService();
        var mainWindow = new MainWindow(message);

        _window = mainWindow;

        _calculator = new Calculator();
        _view = mainWindow;
        _presenter = new CalculatorPresenter(_calculator, _view);
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _window.ShowDialog();
    }
}
