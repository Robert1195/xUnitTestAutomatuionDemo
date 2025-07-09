using EaFramework.Config;
using EaFramework.Reporting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace EaFramework.Driver;

public class DriverWait : IDriverWait
{
    private readonly IDriverFixture _driverFixture;
    private readonly TestSettings _testSettings;
    private readonly Lazy<WebDriverWait> _webDriverWait;
    private readonly IReportManager _reportManager;

    public DriverWait(IDriverFixture driverFixture, TestSettings testSettings, SharedReportFixture fixture)
    {
        _driverFixture = driverFixture;
        _testSettings = testSettings;
        _webDriverWait = new Lazy<WebDriverWait>(GetWaitDriver);
        _reportManager = fixture.ReportManager;
    }

    public IWebElement FindElement(By elementLocator)
    {
        _reportManager.Log($"Finding element with locator {elementLocator}");
        return _webDriverWait.Value.Until(_ => _driverFixture.Driver.FindElement(elementLocator));
    }

    public IEnumerable<IWebElement> FindElements(By elementLocator)
    {
        return _webDriverWait.Value.Until(_ => _driverFixture.Driver.FindElements(elementLocator));
    }

    private WebDriverWait GetWaitDriver()
    {
        return new(_driverFixture.Driver, timeout: TimeSpan.FromSeconds(_testSettings.TimeoutInterval ?? 30))
        {
            PollingInterval = TimeSpan.FromSeconds(_testSettings.TimeoutInterval ?? 1)
        };
    }
}