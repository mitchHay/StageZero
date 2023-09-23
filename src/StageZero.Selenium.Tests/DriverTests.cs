namespace StageZero.Selenium.Tests;

public class Tests
{
    private IDriverWeb _driver;

    [SetUp]
    public void BeforeEach()
    {
        _driver = DriverBuilder.Create(new WebDriverOptions());
    }

    [TestCase("https://www.google.com/")]
    public async Task CanGoToUrl(string url)
    {
        await _driver.GoTo(url);

        Assert.That(_driver.Url, Is.EqualTo(url));
    }

    [TearDown]
    public async Task AfterEach()
    {
        await _driver.Terminate();
    }
}