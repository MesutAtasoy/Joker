using System;
using System.IO;
using Joker.EntityFrameworkCore.Migration;
using Management.Infrastructure;
using Management.Infrastructure.Seed;
using Microsoft.AspNetCore.Hosting;
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

                var host = CreateHostBuilder(args)
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
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

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
            var grpcPort = config.GetValue("GRPC_PORT", 5012);
            var port = 80;
            return (port, grpcPort);
        }
    }
}