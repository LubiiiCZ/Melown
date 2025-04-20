using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Melown.Core;

public static class DriverFactory
{
    public static IWebDriver CreateDriver(bool headless = true)
    {
        new DriverManager().SetUpDriver(new ChromeConfig());
        var chromeOptions = new ChromeOptions();
        chromeOptions.AddArgument("--start-maximized");
        if (headless)
        {
            chromeOptions.AddArgument("--headless");
        }

        var driver = new ChromeDriver(chromeOptions);

        return driver;
    }
}
