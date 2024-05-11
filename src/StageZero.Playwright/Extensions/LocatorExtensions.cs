using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StageZero.Playwright.Extensions;

internal static class LocatorExtensions
{
    internal static async Task<IReadOnlyList<ILocator>> GetAllLocators(this IPage page, string cssSelector)
    {
        const int maxWaitTimeMs = 2500;
        const int maxSearchAttempts = 10;
        const int waitTime = maxWaitTimeMs / maxSearchAttempts;

        IReadOnlyList<ILocator> locators = null;

        var searchAttempts = 0;
        while (locators == null && searchAttempts < maxSearchAttempts)
        {
            try
            {
                locators = await page.Locator(cssSelector).AllAsync();
            }
            catch (Exception)
            {
                Console.WriteLine($"Attempt #{searchAttempts + 1}. Failed to find css selector {cssSelector}. Retrying...");
            }
            finally
            {
                searchAttempts += 1;

                // Wait a couple of ms before retrying the search
                await Task.Delay(waitTime);
            }
        }

        if (locators == null)
        {
            throw new Exception($"Failed to find elements with css selector {cssSelector}");
        }

        return locators;
    }
}

