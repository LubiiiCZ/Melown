using OpenQA.Selenium;

namespace Melown.Core;

public class Element(IWebDriver driver, By by)
{
    public IWebDriver Driver { get; set; } = driver;
    public By By { get; } = by;
    public IWebElement WebElement => Driver.FindWebElement(Driver, By);

    public bool IsPresent()
    {
        return Driver.FindElements(By).Count > 0;
    }
}
