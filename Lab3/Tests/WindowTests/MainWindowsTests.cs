using FluentAssertions;
using MyCalculator.Presenters;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using static Tests.TestUtils.HexExpressionUtils;

namespace Tests.WindowTests;

public class MainWindowsTests : IDisposable
{
    private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";

    private const string AppId =
        @"C:\Users\kondi\Desktop\Labs\Tests\sofrware-testing\Lab3\WpfApp\bin\Debug\net6.0-windows\WpfApp.exe";

    private readonly WindowsDriver<WindowsElement> _session;
    private readonly WindowsElement _multButton;
    private readonly WindowsElement _divButton;
    private readonly WindowsElement _sumButton;
    private readonly WindowsElement _subButton;
    private readonly WindowsElement _firstArg;
    private readonly WindowsElement _secondArg;

    public MainWindowsTests()
    {
        var appCapabilities = new DesiredCapabilities();
        appCapabilities.SetCapability("app", AppId);
        appCapabilities.SetCapability("platformName", "Windows");
        appCapabilities.SetCapability("deviceName ", "WindowsPC");
        _session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities);

        _session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

        _multButton = _session.FindElementByAccessibilityId("MultBtn");
        _divButton = _session.FindElementByAccessibilityId("DivBtn");
        _subButton = _session.FindElementByAccessibilityId("SubBtn");
        _sumButton = _session.FindElementByAccessibilityId("SumBtn");
        _firstArg = _session.FindElementByAccessibilityId("FirstArg");
        _secondArg = _session.FindElementByAccessibilityId("SecondArg");
    }

    #region ClickOnMultiplyButton

    [Fact]
    public void ClickOnMultiplyButton_ShouldShowResult_WhenArgsCanBeParsed()
    {
        const string expected = "4";
        _firstArg.SendKeys("2");
        _secondArg.SendKeys("2");

        _multButton.Click();

        var result = CollectTextMessagesFromDialogWindows();
        result.Should().Equal(expected);
    }

    [Theory]
    [InlineData("", "a")]
    [InlineData(" ", "123B45")]
    public void ClickOnMultiplyButton_ShouldShowResult_WhenArgsCanBeNotParsed(string first, string second)
    {
        var expected = new List<string>
        {
            CalculatorPresenter.FirstArgErrorMessage,
            CalculatorPresenter.SecondArgErrorMessage
        };
        _firstArg.SendKeys(first);
        _secondArg.SendKeys(second);

        _multButton.Click();

        var result = CollectTextMessagesFromDialogWindows();
        result.Should().Equal(expected);
    }

    #endregion

    private IEnumerable<string> CollectTextMessagesFromDialogWindows()
    {
        var result = new List<string>();
        while (true)
        {
            var mainWindowHandle = _session.CurrentWindowHandle;
            var dialogHandles = _session.WindowHandles
                .Where(handle => !CompareAsHexExpression(handle, mainWindowHandle))
                .ToList();

            if (dialogHandles.Count != 1)
                break;
            var handle = dialogHandles.Single();

            _session.SwitchTo().Window(handle);
            result.Add(GetTextAndClose());
            _session.SwitchTo().Window(mainWindowHandle);
        }

        return result;
    }

    private string GetTextAndClose()
    {
        var result = _session.FindElementByAccessibilityId("TextMessage").Text;
        _session.FindElementByAccessibilityId("ClsBtn").Click();
        return result;
    }

    public void Dispose()
    {
        _session.CloseApp();
        _session.Quit();
    }
}