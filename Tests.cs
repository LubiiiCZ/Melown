using OpenQA.Selenium;
using Melown.Pages;
using Melown.Core;
using ImageMagick;

namespace Melown;

public class Tests
{
    protected IWebDriver? WebDriver { get; set; }
    protected StereogramSolver stereogramSolver = null!;
    protected MagickImage expectedShark;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        expectedShark = new MagickImage(Path.Combine(AppContext.BaseDirectory, "Images", "expected-shark.png"));

        WebDriver = DriverFactory.CreateDriver(true);
        stereogramSolver = new(WebDriver!);
        stereogramSolver.NavigateTo();
    }

    [Test, Order(1)]
    public void StereogramSolverPageLoadTest()
    {
        Assert.That(stereogramSolver.Title, Is.EqualTo("Stereogram solver"), "Title does not match.");
    }

    [Test, Order(2)]
    public void StereogramSolverPresetSelectionTest()
    {
        string[] expectedImages = ["shark", "thumbsup", "planet", "dolphins", "atomium"];

        using (Assert.EnterMultipleScope())
        {
            for (int i = 0; i < expectedImages.Length; i++)
            {
                stereogramSolver.SelectPreset(i + 1);
                Assert.That(stereogramSolver.GetSourceImageSrc(), Does.Contain(expectedImages[i]), "Source image src is incorrect.");
            }
        }
    }

    [Test, Order(3)]
    public void StereogramSolverResultComparisonTest()
    {
        stereogramSolver.SelectPreset(1);
        var errorInfo = stereogramSolver.CompareCanvasWithImage(expectedShark);
        Assert.That(errorInfo.NormalizedMeanError, Is.LessThan(0.01), "Images are not similar enough.");
    }

    [Test, Order(4)]
    public void StereogramSolverResultComparisonNegativeTest()
    {
        stereogramSolver.SelectPreset(2);
        var errorInfo = stereogramSolver.CompareCanvasWithImage(expectedShark);
        Assert.That(errorInfo.NormalizedMeanError, Is.GreaterThan(0.01), "Images are not different enough.");
    }

    [Test, Order(5)]
    public void StereogramSolverCustomImageTest()
    {
        stereogramSolver.SelectFile("custom.png");
        var imagePath = Path.Combine(AppContext.BaseDirectory, "Images", "expected-custom.png");
        using var expectedImage = new MagickImage(imagePath);
        var errorInfo = stereogramSolver.CompareCanvasWithImage(expectedImage);
        Assert.That(errorInfo.NormalizedMeanError, Is.LessThan(0.01), "Images are not similar enough.");
    }

    [Test, Order(6)]
    public void StereogramSolverWrongDisplacementTest()
    {
        stereogramSolver.SelectPreset(1);
        stereogramSolver.SetDisplacement(139);
        var errorInfo = stereogramSolver.CompareCanvasWithImage(expectedShark);
        Assert.That(errorInfo.NormalizedMeanError, Is.GreaterThan(0.01), "Images are not different enough.");
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        expectedShark.Dispose();
        WebDriver?.Quit();
        WebDriver?.Dispose();
    }
}
