using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Melown.Core;

public static class DriverFactory
{
    public static IWebDriver CreateDriver(bool headless = true)
    {
        var config = new ChromeConfig();
        config.GetMatchingBrowserVersion();
        new DriverManager().SetUpDriver(config, "135.0.7049.114");

        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--start-maximized");
        chromeOptions.AddArgument("--no-sandbox");
        chromeOptions.AddArgument("--disable-dev-shm-usage");

        if (headless)
        {
            chromeOptions.AddArgument("--headless");
        }

        string tempProfileDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        chromeOptions.AddArgument($"--user-data-dir={tempProfileDir}");

        var driver = new ChromeDriver(chromeOptions);

        return driver;
    }
}
