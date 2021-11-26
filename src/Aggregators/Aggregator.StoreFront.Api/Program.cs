using System.Net;
using Joker.Logging;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Aggregator.StoreFront.Api;

public class Program
{
    /// <summary>
    /// Main
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        var configuration = GetConfiguration();
        Log.Logger = CreateSerilogLogger(configuration, "Aggregator.StoreFront.Api");

        try
        {
            Log.Information("Application starting up...");

            CreateHostBuilder(configuration, args)
                .Build()
                .Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "The application failed to start correctly.");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    /// <summary>
    /// Creates Host Builder
    /// </summary>
    /// <param name="args"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder(IConfiguration configuration, string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                var ports = GetDefinedPorts(configuration);
                    
                webBuilder.UseStartup<Startup>();
                webBuilder.ConfigureKestrel(options =>
                {
                    options.Listen(IPAddress.Any, ports.httpPort, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    });

                    options.Listen(IPAddress.Any, ports.grpcPort, listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http2;
                    });
                });
            })
            .UseSerilog();

    /// <summary>
    /// Returns configuration with environment
    /// </summary>
    /// <returns></returns>
    private static IConfiguration GetConfiguration()
    {
        string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }

    public static ILogger CreateSerilogLogger(IConfiguration configuration, string applicationName)
    {
        return LoggerBuilder.CreateLoggerElasticSearch(x =>
        {
            x.Url = configuration["elk:url"];
            x.BasicAuthEnabled = false;
            x.IndexFormat = "joker-logs";
            x.AppName = applicationName;
            x.Enabled = true;
        });
    }

    public static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
    {
        var grpcPort = config.GetValue("GRPC_PORT", 5040);
        var port = config.GetValue("PORT", 5020);
        return (port, grpcPort);
    }
}