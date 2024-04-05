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
        // Wait for the page to navigate
        await Task.Delay(250);

        var js = "return document.location.href;";
        var href = await Driver.Document().ExecuteJavaScript<string>(js);
        Assert.That(href, Is.Not.Null);
        Assert.That(href, Is.Not.Empty);
    }
}
