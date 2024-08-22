using OpenQA.Selenium;
using System;
using System.Threading.Tasks;

namespace StageZero.Selenium
{
    public sealed class SeleniumNavigate : INavigate
    {
        private readonly IWebDriver _driver;

        public SeleniumNavigate(IWebDriver driver)
        {
            _driver = driver;
        }

        /// <inheritdoc/>
        public async Task Back()
        {
            await _driver.Navigate().BackAsync();
        }

        /// <inheritdoc/>
        public async Task Forward()
        {
            await _driver.Navigate().ForwardAsync();
        }

        /// <inheritdoc/>
        public async Task ToUrl(string url)
        {
            await _driver.Navigate().GoToUrlAsync(url);
        }

        /// <inheritdoc/>
        public async Task ToUrl(Uri uri)
        {
            await _driver.Navigate().GoToUrlAsync(uri);
        }
    }
}
