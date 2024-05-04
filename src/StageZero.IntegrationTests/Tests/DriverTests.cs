namespace StageZero.IntegrationTests.Tests;

public class DriverTests : TestBase
{
    private const string UrlToNavigate = "https://www.google.com/";

    public DriverTests(Type driverBuilderType) : base(driverBuilderType)
    {
    }

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

        Assert.That(string.IsNullOrEmpty(inputValue), Is.True);
    }


    [Test]
    public async Task CanOpenAlert() 
    {
        // Listen for alerts
        Driver.OnAlert += async (_, alert) =>
        {
            Assert.That(alert.Message, Is.EqualTo("Alert opened!"));
            await alert.Dismiss();
        };

        // Navigate through test
        await Driver.Navigate().ToUrl(TestSitePath);

        var alertButton = await Driver.GetElement("#test-alert-button");
        await alertButton.Click();
    }
}