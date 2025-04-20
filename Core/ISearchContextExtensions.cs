using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Melown.Core;

public static class ISearchContextExtensions
{
    public static void WaitForElement(this ISearchContext searchContext, IWebDriver driver, By by)
    {
        WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
        wait.TryUntil(_ => searchContext.FindElement(by));
    }

    public static IWebElement FindWebElement(this ISearchContext searchContext, IWebDriver driver, By by)
    {
        WaitForElement(searchContext, driver, by);
        return searchContext.FindElement(by);
    }
}
