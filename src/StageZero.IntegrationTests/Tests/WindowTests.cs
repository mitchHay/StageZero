namespace StageZero.IntegrationTests.Tests;

public class WindowTests : TestBase
{
    public WindowTests(Type driverBuilderType) : base(driverBuilderType)
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

    [Test]
    public void CanGetCurrentHandle()
    {
        Assert.That(Driver.Window().CurrentHandle, Is.Not.Null);
    }

    [Test]
    public void CanGetHandles()
    {
        var handles = Driver.Window().Handles;

        Assert.That(handles, Is.Not.Null);
        Assert.That(handles, Is.Not.Empty);
    }
}

