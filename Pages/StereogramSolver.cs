using ImageMagick;
using Melown.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Melown.Pages;

public class StereogramSolver(IWebDriver driver)
{
    public static string Url => "https://piellardj.github.io/stereogram-solver/";
    public IWebDriver Driver { get; set; } = driver;
    public IJavaScriptExecutor Js { get; set; } = (IJavaScriptExecutor)driver;
    public WebDriverWait Wait { get; set; } = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
    public string Title => Driver.Title;
    public IWebElement PresetSelect => Driver.FindWebElement(Driver, By.Id("preset-select"));
    public IWebElement FileInput => Driver.FindWebElement(Driver, By.Id("image-upload"));
    public IWebElement DisplacementInput => Driver.FindWebElement(Driver, By.ClassName("full-width-range"));
    public IWebElement SourceImage => Driver.FindWebElement(Driver, By.CssSelector("img"));

    public void NavigateTo()
    {
        Driver.Navigate().GoToUrl(Url);
    }

    public void SelectPreset(int index)
    {
        SelectElement select = new(PresetSelect);
        select.SelectByIndex(index);
        Thread.Sleep(1000); // Wait for the JavaScript to process the selection
    }

    public IMagickErrorInfo CompareCanvasWithImage(MagickImage image)
    {
        using var canvasImage = new MagickImage(GetCanvasBytes());
        return canvasImage.Compare(image);
    }

    public void SetDisplacement(int value)
    {
        Js.ExecuteScript($@"
            const slider = arguments[0];
            slider.value = {value};
            slider.dispatchEvent(new Event('input', {{ bubbles: true }}));
            slider.dispatchEvent(new Event('change', {{ bubbles: true }}));
        ", DisplacementInput);

        Thread.Sleep(1000); // Wait for the JavaScript to process the input
    }

    public void SelectFile(string fileName)
    {
        string filePath = Path.Combine(AppContext.BaseDirectory, "Images", fileName);
        FileInput.SendKeys(filePath);
        Thread.Sleep(1000); // Wait for the JavaScript to process the file input
    }

    public string GetSourceImageSrc()
    {
        return SourceImage.GetAttribute("src") ?? string.Empty;
    }

    public byte[] GetCanvasBytes()
    {
        string actualDataUrl = Js.ExecuteScript("return document.getElementsByTagName('canvas')[0].toDataURL();") as string ?? string.Empty;
        string base64Data = actualDataUrl.Split(',')[1];
        byte[] imageBytes = Convert.FromBase64String(base64Data);
        return imageBytes;
    }
}
