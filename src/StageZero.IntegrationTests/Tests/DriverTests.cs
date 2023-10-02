namespace StageZero.IntegrationTests.Tests;

public class DriverTests : TestBase
{
    private const string UrlToNavigate = "https://www.google.com/";

    [Test(ExpectedResult = true)]
    public bool DriverIsDefined()
    {
        return Driver != null;
    }

    [TestCase(UrlToNavigate)]
    public async Task CanNavigateToUrlFromString(string url)
    {
        await Driver.Navigate().ToUrl(url);

        Assert.That(Driver.Url, Is.EqualTo(url));
    }

    [Test]
    public async Task CanNavigateToUrlFromUri()
    {
        await Driver.Navigate().ToUrl(new Uri(UrlToNavigate));

        Assert.That(Driver.Url, Is.EqualTo(UrlToNavigate));
    }
}