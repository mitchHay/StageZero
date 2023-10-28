namespace StageZero.IntegrationTests.Tests;

public class DocumentTests : TestBase
{
    public DocumentTests(Type driverBuilderType) : base(driverBuilderType)
    {
        ShouldNavigateToTestSite = true;
    }

    [Test]
    public async Task CanExecuteJs()
    {
        if (Driver.GetType() == typeof(Playwright.WebDriver))
        {
            Assert.Ignore($"Playwright JS execution still needs to be fleshed out. Skipping {nameof(CanExecuteJs)} for now.");
        }

        // Wait for the page to navigate
        await Task.Delay(250);

        var inputElementByJs = await Driver.Document().ExecuteJavaScript<IElementWeb>("return document.getElementById('test-input')");
        Assert.That(inputElementByJs, Is.Not.Null);
    }
}
