using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Lab5;

public class SiteTests : IDisposable
{
    private readonly IWebDriver _webDriver;

    public SiteTests()
    {
        _webDriver = new OpenQA.Selenium.Chrome.ChromeDriver();
        _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        _webDriver.Manage().Window.Maximize();
    }

    [Fact]
    public void Registration_ShouldShowFailed_WhenPhoneNumberIsNotCorrected()
    {
        _webDriver.Navigate().GoToUrl(@"https://ok.ru/dk?cmd=AnonymRegistrationEnterPhone&" +
                                      @"st.cmd=anonymRegistrationEnterPhone&st.cmd=anonymRegistrationEnterPhone");
        var phoneInput = _webDriver.FindElement(By.Name("st.r.phone"));
        phoneInput.SendKeys("qwerty");
        var registrationButton = _webDriver.FindElement(By.XPath("/html/body"));

        registrationButton.Click();

        var message = _webDriver.FindElement(By.ClassName("input-e"));
        message.Text.Should().Be("Неправильный номер телефона.");
    }

    [Fact]
    public void FindVideo_ShouldShowEmptyList_WhenRequestIsNotCorrected()
    {
        _webDriver.Navigate().GoToUrl("https://vk.com/video");
        var searchInput = _webDriver.FindElement(By.Id("video_search_input"));
        searchInput.SendKeys("/");

        var result = _webDriver.FindElement(By.ClassName("VideoEmptyStub__text"));

        Thread.Sleep(500);
        result.Text.Should().Be("По запросу /\r\nне найдено ни одного видео");
    }

    [Fact]
    public void SelectGroup_ShouldShowNoEmptySchedule_WhenGroupNameIsCorrected()
    {
        _webDriver.Navigate().GoToUrl("https://www.miet.ru/schedule");
        var groups = _webDriver.FindElement(By.ClassName("group"));
        var selector = new SelectElement(groups);

        selector.SelectByValue("ПИН-44");

        var result = _webDriver.FindElement(By.ClassName("data"));
        result.Text.Should().NotBeEmpty();
    }

    public void Dispose()
    {
        _webDriver.Dispose();
    }
}