namespace StageZero.IntegrationTests.Tests;

public class ElementTests : TestBase
{
    public ElementTests()
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
}

