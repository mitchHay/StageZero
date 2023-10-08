using OpenQA.Selenium;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace StageZero.Selenium
{
    public sealed class Window : Web.IWindow
    {
        private readonly IWebDriver _driver;
        private readonly IWindow _window;

        /// <inheritdoc/>
        public string CurrentHandle => _driver.CurrentWindowHandle;

        /// <inheritdoc/>
        public IEnumerable<string> Handles => _driver.WindowHandles;

        /// <inheritdoc/>
        public Size Size => _window.Size;

        public Window(IWebDriver driver)
        {
            _driver = driver;
            _window = driver.Manage().Window;
        }

        /// <inheritdoc/>
        public Task Fullscreen()
        {
            return Task.Run(
                () => _window.FullScreen()
            );
        }

        /// <inheritdoc/>
        public Task SetSize(int width, int height)
        {
            return Task.Run(() =>
            {
                _window.Size = new Size(width, height);
            });
        }
    }
}
