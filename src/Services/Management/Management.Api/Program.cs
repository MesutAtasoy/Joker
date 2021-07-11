using System;
using System.IO;
using System.Net;
using Joker.EntityFrameworkCore.Migration;
using Management.Infrastructure;
using Management.Infrastructure.Seed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Management.Api
{
    public class Program
    {
       /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            Log.Logger = CreateSerilogLogger(configuration, "Management.Api");

            try
            {
                Log.Information("Application starting up...");

                var host = CreateHostBuilder(configuration, args)
                    .Build();

                host.MigrateDbContext<ManagementContext>((context, services) =>
                {
                    new ManagementContextSeeder().SeedAsync(options =>
                    {
                        var env = services.GetService<IHostEnvironment>();
                        var logger = services.GetService<ILogger<ManagementContext>>();
                        options.Context = context;
                        options.ContentRootPath = env?.ContentRootPath;
                        options.Logger = logger;
                        options.RetryCount = 5;
                    },services).Wait();
                });

                host.Run();
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
            return new LoggerConfiguration()
                .Enrich.WithProperty("ApplicationContext", applicationName)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
        
        public static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
        {
            var isValidGrpcPort = int.TryParse(config["GRPC_PORT"], out var grpcPort);
            if (!isValidGrpcPort || grpcPort <= 0)
            {
                grpcPort = 5012;
            }
            var isValidPort = int.TryParse(config["PORT"], out var port);
            if (!isValidPort || port <= 0)
            {
                port = 5002;
            }
            
            return (port, grpcPort);
        }
    }
}