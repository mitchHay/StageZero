namespace StageZero.IntegrationTests.Tests;

public class WindowTests : TestBase
{
    public WindowTests()
    {
        ShouldNavigateToTestSite = true;
    }

    [Test]
    public async Task CanResize()
    {
        await Driver.Window().SetSize(400, 400);

        var windowSize = Driver.Window().Size;

        Assert.Multiple(() =>
        {
            Assert.That(windowSize.Width, Is.EqualTo(400));
            Assert.That(windowSize.Height, Is.EqualTo(400));
        });
    }
}

