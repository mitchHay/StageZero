namespace StageZero.Selenium.Tests;

[Parallelizable(ParallelScope.All)]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
public class Tests
{
    private IDriverWeb _driver;

    private const string UrlToNavigate = "https://www.google.com/";

    [SetUp]
    public void BeforeEach()
    {
        _driver = DriverBuilder.Create(new WebDriverOptions
        {
            Headless = true
        });
    }

    [TestCase(UrlToNavigate)]
    public async Task CanNavigateToUrlFromString(string url)
    {
        await _driver.Navigate().ToUrl(url);

        Assert.That(_driver.Url, Is.EqualTo(url));
    }

    [Test]
    public async Task CanNavigateToUrlFromUri()
    {
        await _driver.Navigate().ToUrl(new Uri(UrlToNavigate));

        Assert.That(_driver.Url, Is.EqualTo(UrlToNavigate));
    }

    [TearDown]
    public async Task AfterEach()
    {
        await _driver.Terminate();
    }
}