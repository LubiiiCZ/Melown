using Allure.Commons;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using OpenQA.Selenium;
using Melown.Pages;
using Melown.Core;

namespace Melown;

[AllureNUnit]
public class Tests
{
    protected IWebDriver? WebDriver { get; set; }
    protected StereogramSolver stereogramSolver = null!;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        WebDriver = DriverFactory.CreateDriver(true);
        stereogramSolver = new(WebDriver!);
    }

    [AllureOwner("LD for Melown")]
    [AllureTag("Regression")]
    [AllureSeverity(SeverityLevel.critical)]
    [Test, Order(1)]
    public void StereogramSolverPageLoadTest()
    {
        stereogramSolver.NavigateTo();
        Assert.That(stereogramSolver.Title, Is.EqualTo("Stereogram solver"), "Title does not match.");
    }

    [AllureOwner("LD for Melown")]
    [AllureTag("Regression")]
    [AllureSeverity(SeverityLevel.normal)]
    [Test, Order(2)]
    public void StereogramSolverPresetSelectionTest()
    {
        string[] expectedImages = ["shark", "thumbsup", "planet", "dolphins", "atomium"];

        using (Assert.EnterMultipleScope())
        {
            for (int i = 0; i < expectedImages.Length; i++)
            {
                stereogramSolver.SelectPreset(i + 1);
                Thread.Sleep(500); //process javaScript
                Assert.That(stereogramSolver.GetSourceImageSrc(), Does.Contain(expectedImages[i]), "Source image src is incorrect.");
            }
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Thread.Sleep(2000); //demo
        WebDriver?.Quit();
        WebDriver?.Dispose();
    }
}
