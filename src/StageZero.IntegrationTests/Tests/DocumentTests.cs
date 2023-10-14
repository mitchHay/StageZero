namespace StageZero.IntegrationTests.Tests;

public class DocumentTests : TestBase
{
    public DocumentTests()
    {
        ShouldNavigateToTestSite = true;
    }

    [Test]
    public async Task CanExecuteJs()
    {
        var inputElementByJs = await Driver.Document().ExecuteJavaScript<IElementWeb>("return document.getElementById('test-input')");
        Assert.That(inputElementByJs, Is.Not.Null);
    }
}
