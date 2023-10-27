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
        // Invoke a "wait" for the element to be displayed
        _ = Driver.GetElement("#test-input");

        var inputElementByJs = await Driver.Document().ExecuteJavaScript<IElementWeb>("return document.getElementById('test-input')");
        Assert.That(inputElementByJs, Is.Not.Null);
    }
}
