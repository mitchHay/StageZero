using StageZero.Web;
using System;

namespace StageZero;

public class DriverBuilder
{
    private static IDriverBuilder _driverBuilder;

    public static void Register<TDriverBuilder>()
        where TDriverBuilder : IDriverBuilder, new()
    {
        _driverBuilder = new TDriverBuilder();
    }

    /// <summary>
    /// Create a new <see cref="IDriverWeb"/> instance from a registered driver type.
    /// </summary>
    /// <param name="options"><see cref="WebDriverOptions"/> to provide the driver instance.</param>
    /// <returns>A new <see cref="IDriverWeb"/> instance.</returns>
    public static IDriverWeb Create(WebDriverOptions options)
    {
        return (IDriverWeb)_driverBuilder.Create(options);
    }
}
