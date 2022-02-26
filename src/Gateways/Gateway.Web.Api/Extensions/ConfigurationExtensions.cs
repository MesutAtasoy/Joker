namespace Gateway.Web.Api.Extensions;

public static class ConfigurationExtensions
{ 
    public static IConfiguration GetConfiguration()
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddJsonFile($"configuration.{environmentName}.json")
            .AddEnvironmentVariables();

        return builder.Build();
    }
}