using Melown.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Melown.Pages;

public class StereogramSolver(IWebDriver driver)
{
    public static string Url => "https://piellardj.github.io/stereogram-solver/";
    public IWebDriver Driver { get; set; } = driver;
    public WebDriverWait Wait { get; set; } = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    public string Title => Driver.Title;
    public IWebElement PresetSelect => Driver.FindWebElement(Driver, By.Id("preset-select"));
    public IWebElement SourceImage => Driver.FindWebElement(Driver, By.CssSelector("img"));

    public void NavigateTo()
    {
        Driver.Navigate().GoToUrl(Url);
    }

    public void SelectPreset(int index)
    {
        SelectElement select = new(PresetSelect);
        select.SelectByIndex(index);
    }

    public string GetSourceImageSrc()
    {
        return SourceImage.GetAttribute("src") ?? string.Empty;
    }
}
