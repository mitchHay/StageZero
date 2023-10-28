using Microsoft.Playwright;
using StageZero.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StageZero.Playwright
{
    public sealed class Window : IWindow
    {
        private readonly IPage _page;

        public string CurrentHandle => _page.TitleAsync().Result;

        public IEnumerable<string> Handles => _page.Context.Pages.Select(p => p.TitleAsync().Result);

        public Size Size => new(_page.ViewportSize.Width, _page.ViewportSize.Height);

        public Window(IPage page)
        {
            _page = page;
        }

        public async Task Fullscreen()
        {
            await _page.SetViewportSizeAsync(0, 0);
        }

        public async Task SetSize(int width, int height)
        {
            await _page.SetViewportSizeAsync(width, height);
        }
    }
}
