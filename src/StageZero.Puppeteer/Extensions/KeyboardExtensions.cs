using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PuppeteerSharp.Input;
using StageZero.Web;

namespace StageZero.Puppeteer.Extensions;

internal enum KeyPress
{
    Up,
    Down,
}

internal static class KeyboardExtensions
{
    internal static async Task SendKeyEvent(this IKeyboard keyboard, Keys keys, KeyPress keyPress)
    {
        foreach (Keys key in Enum.GetValues(keys.GetType()))
        {
            if (!keys.HasFlag(key))
            {
                continue;
            }

            switch (keyPress)
            {
                case KeyPress.Up:
                    await keyboard.UpAsync(key.ToString());
                    break;
                case KeyPress.Down:
                    await keyboard.DownAsync(key.ToString());
                    break;
            }            
        }
    }
}