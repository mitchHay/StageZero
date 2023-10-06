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

    [Test]
    public async Task CanRefresh()
    {
        await Driver.Navigate().ToUrl(TestSitePath);

        var testInputElement = await Driver.GetElement("#test-input");
        await testInputElement.Type("Testing");

        await Driver.Refresh();

        // Re-wait for the element again because we just invoked a refresh and it may not be there
        testInputElement = await Driver.GetElement("#test-input");
        var inputValue = await testInputElement.GetAttributeValue("value");

        Assert.That(inputValue, Is.Empty);
    }
}