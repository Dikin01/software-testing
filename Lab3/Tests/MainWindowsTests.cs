using FluentAssertions;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace Tests;

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

        _session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

        _multButton = _session.FindElementByAccessibilityId("MultBtn");
        _divButton = _session.FindElementByAccessibilityId("DivBtn");
        _subButton = _session.FindElementByAccessibilityId("SubBtn");
        _sumButton = _session.FindElementByAccessibilityId("SumBtn");
        _firstArg  = _session.FindElementByAccessibilityId("FirstArg");
        _secondArg  = _session.FindElementByAccessibilityId("SecondArg");
    }

    [Fact]
    public void ClickOnMultiplyButton_ShouldShowResult_WhenArgsCanBeParsed()
    {
        const string expected = "4";
        _firstArg.SendKeys("2");
        _secondArg.SendKeys("2");
        
        _multButton.Click();
        _session.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        
        var mainWindowHandle = _session.CurrentWindowHandle;
        var windowHandles = _session.WindowHandles
            .Where(handle => !CompareAsHexExpression(handle, mainWindowHandle))
            .ToList();
        windowHandles.Should().HaveCount(1);
        foreach (var windowHandle in windowHandles)
        {
            _session.SwitchTo().Window(windowHandle);
            var resultElement = _session.FindElementByAccessibilityId("MessageText");
            resultElement.Text.Should().Be(expected);
        }
    }

    private static bool CompareAsHexExpression(string a, string b)
    {
        var aBytes = ConvertToBytes(a);
        var bBytes = ConvertToBytes(b);
        return aBytes.SequenceEqual(bBytes);
    }

    private static byte[] ConvertToBytes(string hexExpression)
    {
        var expression = hexExpression.Trim();
        if (!expression.StartsWith("0x"))
            throw new FormatException("Expression should start with 0x");
        expression = new string(expression
            .Skip(2)
            .SkipWhile(symbol => symbol == '0')
            .ToArray());

        var range = Enumerable.Range(0, expression.Length / 2);
        var result = new List<byte>();
        foreach (var item in range)
        {
            var partExpression = expression.Substring(item * 2, 2);
            var itemByte = Convert.ToByte(partExpression, 16);
            result.Add(itemByte);
        }

        return result.ToArray();
    }


    public void Dispose()
    {
        _session.CloseApp();
        _session.Quit();
    }
}