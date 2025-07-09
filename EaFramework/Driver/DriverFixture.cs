using EaFramework.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.Extensions;
using System.Reflection;

namespace EaFramework.Driver;

public class DriverFixture : IDriverFixture, IDisposable
{
    private readonly TestSettings _testSettings;

    public IWebDriver Driver { get; }
    
    public DriverFixture(TestSettings testSettings)
    {
        _testSettings = testSettings;
        Driver = _testSettings.TestRunType == TestRunType.Local ? GetWebDriver() : GetRemoteWebDriver();
        Driver.Navigate().GoToUrl(_testSettings.ApplicationUrl);
    }


    private IWebDriver GetWebDriver()
    {
        return _testSettings.BrowserType switch
        {
            BrowserType.Chrome => CreateChromeDriver(),
            BrowserType.Firefox => CreateFirefoxDriver(),
            BrowserType.EdgeChromium => CreateEdgeDriver(),
            BrowserType.Safari => new SafariDriver(),
            _ => new ChromeDriver()
        };
    }

    private IWebDriver GetRemoteWebDriver()
    {
        return _testSettings.BrowserType switch
        {
            BrowserType.Chrome => new RemoteWebDriver(_testSettings.GridUri, new ChromeOptions()),
            BrowserType.Firefox => new RemoteWebDriver(_testSettings.GridUri, new FirefoxOptions()),
            BrowserType.Safari => new RemoteWebDriver(_testSettings.GridUri, new SafariOptions()),
            _ => new ChromeDriver()
        };
    }

    private IWebDriver CreateChromeDriver()
    {
        var chromeOptions = CreateChromeOptions();
        return new ChromeDriver(chromeOptions);
    }

    private ChromeOptions CreateChromeOptions()
    {
        var chromeOptions = new ChromeOptions();
        if (_testSettings.Headless)
        {
            chromeOptions.AddArgument("--headless=new"); // Nowszy sposób ustawiania headless w Chrome
            chromeOptions.AddArgument("--disable-gpu"); // Zalecane dla headless
            chromeOptions.AddArgument("--window-size=1920,1080"); // Ustaw rozdzielczość
        }
        return chromeOptions;
    }

    private IWebDriver CreateFirefoxDriver()
    {
        var firefoxOptions = CreateFirefoxOptions();
        return new FirefoxDriver(firefoxOptions);
    }

    private FirefoxOptions CreateFirefoxOptions()
    {
        var firefoxOptions = new FirefoxOptions();
        if (_testSettings.Headless)
        {
            firefoxOptions.AddArgument("--headless");
            firefoxOptions.AddArgument("--width=1920");
            firefoxOptions.AddArgument("--height=1080");
        }
        return firefoxOptions;
    }

    private IWebDriver CreateEdgeDriver()
    {
        var edgeOptions = CreateEdgeOptions();
        return new EdgeDriver(edgeOptions);
    }

    private EdgeOptions CreateEdgeOptions()
    {
        var edgeOptions = new EdgeOptions();
        if (_testSettings.Headless)
        {
            edgeOptions.AddArgument("--headless");
            edgeOptions.AddArgument("--disable-gpu");
            edgeOptions.AddArgument("--window-size=1920,1080");
        }
        return edgeOptions;
    }

    public string TakeScreenshotAsPath(string fileName)
    {
        var screenshot = Driver.TakeScreenshot();
        var path = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}//{fileName}.png";
        screenshot.SaveAsFile(path);
        return path;
    }

    public void Dispose()
    {
       Driver.Quit();
    }
}


public enum BrowserType
{
    Chrome,
    Firefox,
    Safari,
    EdgeChromium
}
