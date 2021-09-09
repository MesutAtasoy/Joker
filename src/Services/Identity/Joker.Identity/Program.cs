using System;
using System.IO;
using Joker.EntityFrameworkCore.Migration;
using Joker.Identity.Models;
using Joker.Identity.Models.Seeders;
using Joker.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using ILogger = Serilog.ILogger;

namespace Joker.Identity
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
            Log.Logger = CreateSerilogLogger(configuration, "Identity.Api");

            try
            {
                Log.Information("Application starting up...");

                var host = CreateHostBuilder(args)
                    .Build();

                host.MigrateDbContext<JokerIdentityDbContext>((context, services) =>
                {
                    new JokerIdentityDbContextSeeder().SeedAsync(x =>
                    {
                        var logger = services.GetService<ILogger<JokerIdentityDbContext>>();
                        x.Context = context;
                        x.Logger = logger;
                        x.RetryCount = 5;
                    }, services).Wait();
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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();

        /// <summary>
        /// Returns configuration with environment
        /// </summary>
        /// <returns></returns>
        private static IConfiguration GetConfiguration()
        {
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            Console.WriteLine(environmentName);
            
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
    }
}