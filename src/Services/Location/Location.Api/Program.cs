using Elastic.Apm.AspNetCore;
using Joker.Configuration;
using Joker.EntityFrameworkCore.Migration;
using Joker.Logging;
using Joker.Mvc;
using Location.Api.Extensions;
using Location.Api.GrpcServices;
using Location.Application;
using Location.Infrastructure;
using Location.Infrastructure.Seed;
using Serilog;

var configuration = JokerConfigurationHelper.GetConfiguration();

Log.Logger = LoggerBuilder.CreateLoggerElasticSearch(x =>
{
    x.Url = configuration["elk:url"];
    x.BasicAuthEnabled = false;
    x.IndexFormat = "joker-logs";
    x.AppName = "Location.Api";
    x.Enabled = true;
    x.Configuration = configuration;
    x.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
});


try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost.BuildKestrel(configuration);

    var services = builder.Services;
    
    services.AddApiVersion();
    services.AddJokerGrpc();
    services.AddControllers();
    services.AddJokerContext(configuration);
    services.AddApplicationModule();
    services.AddJokerMediatr(typeof(LocationApplicationModule));
    services.AddSwaggerGen();
    services.AddJokerConsul(configuration);
    services.AddJokerOpenTelemetry(configuration);

    builder.Host.UseSerilog();

    var app = builder.Build();

    app.MigrateDbContext<LocationContext>((context, serviceProvider) =>
    {
        new LocationContextSeeder().SeedAsync(options =>
        {
            var env = serviceProvider.GetService<IHostEnvironment>();
            var logger = serviceProvider.GetService<ILogger<LocationContext>>();
            options.Context = context;
            options.ContentRootPath = env?.ContentRootPath;
            options.Logger = logger;
            options.RetryCount = 5;
        }, serviceProvider).Wait();
    });

    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Location.Api v1"));
    app.UseElasticApm(configuration);
    app.UseErrorHandler();
    app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapControllers();
        endpoints.MapGrpcService<LocationGrpcService>();
    });

    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "The application failed to start correctly");
}
finally
{
    Log.CloseAndFlush();
}