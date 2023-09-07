using System;

namespace StageZero;

public static class DriverBuilder
{
    public static TDriver Initialise<TDriver>(DriverOptions options) where TDriver : IDriver
    {
        return (TDriver)Activator.CreateInstance(typeof(TDriver), options);
    }
}
