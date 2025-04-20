using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Melown.Pages;

public class StereogramSolver(IWebDriver driver)
{
    public static string Url => "https://piellardj.github.io/stereogram-solver/";
    public IWebDriver Driver { get; set; } = driver;
    public WebDriverWait Wait { get; set; } = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    public string Title => Driver.Title;

    public void NavigateTo()
    {
        Driver.Navigate().GoToUrl(Url);
    }
}
