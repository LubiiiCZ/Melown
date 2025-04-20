using OpenQA.Selenium;
using Melown.Pages;
using Melown.Core;

namespace Melown;

public class Tests
{
    protected IWebDriver? WebDriver { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        WebDriver = DriverFactory.CreateDriver(false);
    }

    [Test]
    public void StereogramSolverTest()
    {
        using (Assert.EnterMultipleScope())
        {
            StereogramSolver homePage = new(WebDriver!);
            homePage.NavigateTo();
            Assert.That(homePage.Title, Is.EqualTo("Stereogram solver"), "Title does not match.");
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        Thread.Sleep(5000);
        WebDriver?.Quit();
        WebDriver?.Dispose();
    }
}
