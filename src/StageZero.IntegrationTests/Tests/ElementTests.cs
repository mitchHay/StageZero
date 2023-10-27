namespace StageZero.IntegrationTests.Tests;

public class ElementTests : TestBase
{
    public ElementTests(Type driverBuilderType) : base(driverBuilderType)
    {
        ShouldNavigateToTestSite = true;
    }

    [Test]
    public async Task CanGetElement()
    {
        var bodyElement = await Driver.GetElement("html body");
        Assert.That(bodyElement, Is.Not.Null);
    }

    [TestCase("Testing 123")]
    public async Task CanType(string textToType)
    {
        var inputElement = await Driver.GetElement("#test-input");
        await inputElement.Type(textToType);

        var inputValue = await inputElement.GetAttributeValue("value");
        Assert.That(inputValue, Is.EqualTo(textToType));
    }

    [Test(ExpectedResult = "Clicked 1 times!")]
    public async Task<string> CanClick()
    {
        var buttonElement = await Driver.GetElement("#test-button");
        await buttonElement.Click();

        return buttonElement.Text;
    }

    [Test(ExpectedResult = "Clicked 2 times!")]
    public async Task<string> CanDoubleClick()
    {
        var buttonElement = await Driver.GetElement("#test-button");
        await buttonElement.DoubleClick();

        return buttonElement.Text;
    }

    [Test(ExpectedResult = "Right click successful!")]
    public async Task<string> CanRightClick()
    {
        var menuElement = await Driver.GetElement("#test-menu");
        await menuElement.RightClick();

        var successText = (await Driver.GetElement("#test-menu-success")).Text;
        return successText;
    }

    [Test(ExpectedResult = "Held successfully!")]
    public async Task<string> CanHold()
    {
        var holdButtonElement = await Driver.GetElement("#test-button-hold");
        await holdButtonElement.ClickAndHold(TimeSpan.FromSeconds(1));

        return holdButtonElement.Text;
    }

    [Test(ExpectedResult = "CTRL + B DETECTED!")]
    public async Task<string> CanPressKeys()
    {
        var bodyElement = await Driver.GetElement("body");
        await bodyElement.PressKeys(Keys.Control | Keys.B);

        var alertText = (await Driver.GetElement("#alert-text")).Text;
        return alertText;
    }

    [Test(ExpectedResult = "test-class")]
    public async Task<string> CanGetClassName()
    {
        var testClassElement = await Driver.GetElement(".test-class");
        return testClassElement.ClassName;
    }

    [Test(ExpectedResult = "test-id")]
    public async Task<string> CanGetId()
    {
        var testIdElement = await Driver.GetElement("#test-id");
        return testIdElement.Id;
    }

    [Test(ExpectedResult = "test-tag")]
    public async Task<string> CanGetTagName()
    {
        var testTagElement = await Driver.GetElement("#test-tag");
        return testTagElement.Tag;
    }
}

