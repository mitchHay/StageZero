using Microsoft.Extensions.Configuration;

namespace StageZero.IntegrationTests.Helpers;

public static class AppSettings
{
    private static readonly IConfiguration _config;

    static AppSettings()
    {
        _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.User.json", optional: true)
                .Build();
    }

    public static TResult Get<TResult>(string key)
    {
        return _config.GetValue<TResult>(key) 
            ?? throw new NullReferenceException($"Failed to retrieve setting from appsettings.json file with key {key}");
    }
}
