using FluentAssertions;
using OpenQA.Selenium;

namespace Lab5;

public class SiteTests : IDisposable
{
    private readonly IWebDriver _webDriver;

    public SiteTests()
    {
        _webDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
        _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        _webDriver.Navigate().GoToUrl(@"https://ok.ru/dk?cmd=AnonymRegistrationEnterPhone&" + 
                                      @"st.cmd=anonymRegistrationEnterPhone&st.cmd=anonymRegistrationEnterPhone");
        _webDriver.Manage().Window.Maximize();
    }
    
    [Fact]
    public void Registration_ShouldBeFailed_WhenPhoneNumberIsNotCorrected()
    {
        var phoneInput = _webDriver.FindElement(By.Name("st.r.phone"));
        phoneInput.SendKeys("910835174");
        var registrationButton = _webDriver.FindElement(By.XPath("/html/body"));
        
        registrationButton.Click();

        var message = _webDriver.FindElement(By.ClassName("input-e"));
        message.Text.Should().Be("Неправильный номер телефона.");
    }

    public void Dispose()
    {
        _webDriver.Dispose();
    }
}