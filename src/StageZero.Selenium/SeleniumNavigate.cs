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
        public Task Back()
        {
            return Task.Run(() => _driver.Navigate().Back());
        }

        /// <inheritdoc/>
        public Task Forward()
        {
            return Task.Run(() => _driver.Navigate().Forward());
        }

        /// <inheritdoc/>
        public Task ToUrl(string url)
        {
            return Task.Run(() => _driver.Navigate().GoToUrl(url));
        }

        /// <inheritdoc/>
        public Task ToUrl(Uri uri)
        {
            return Task.Run(() => _driver.Navigate().GoToUrl(uri));
        }
    }
}
