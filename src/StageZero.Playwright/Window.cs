using Microsoft.Playwright;
using StageZero.Web;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace StageZero.Playwright
{
    public sealed class Window : IWindow
    {
        private readonly IPage _page;

        /// <inheritdoc/>
        public string CurrentHandle => _page.TitleAsync().Result;

        /// <inheritdoc/>
        public IEnumerable<string> Handles => _page.Context.Pages.Select(p => p.TitleAsync().Result);

        /// <inheritdoc/>
        public Size Size => new(_page.ViewportSize.Width, _page.ViewportSize.Height);

        public Window(IPage page)
        {
            _page = page;
        }

        /// <inheritdoc/>
        public async Task Fullscreen()
        {
            await _page.SetViewportSizeAsync(0, 0);
        }

        /// <inheritdoc/>
        public async Task SetSize(int width, int height)
        {
            await _page.SetViewportSizeAsync(width, height);
        }
    }
}
