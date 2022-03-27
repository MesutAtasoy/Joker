using Elastic.Apm.AspNetCore;
using Joker.Configuration;
using Joker.Logging;
using Joker.Mvc;
using Joker.Mvc.Initializers;
using Search.Api.Extensions;
using Search.Application;
using Search.Core;
using Serilog;

var configuration = JokerConfigurationHelper.GetConfiguration();
Log.Logger = LoggerBuilder.CreateLoggerElasticSearch(x =>
{
    x.Url = configuration["elk:url"];
    x.BasicAuthEnabled = false;
    x.IndexFormat = "joker-logs";
    x.AppName = "Search.Api";
    x.Enabled = true;
    x.Configuration = configuration;
    x.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
});

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    var services = builder.Services;
    services.AddApiVersion();
    services.AddControllers();
    services.AddApplicationModule();
    services.AddCoreModule();
    services.AddJokerMediatr(typeof(SearchApplicationModule));
    services.AddSwaggerGen();
    services.AddElasticService(configuration);
    services.AddJokerEventBus(configuration);
    services.AddJokerConsul(configuration);
    services.AddJokerOpenTelemetry(configuration);

    builder.Host.UseSerilog();
    
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search.Api v1"));
    app.UseElasticApm(configuration);
    app.UseErrorHandler();
    app.UseRouting();
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

    await using (var scope = app.Services.CreateAsyncScope())
    {
        var initializer = scope.ServiceProvider.GetRequiredService<IStartupInitializer>();
        await initializer.InitializeAsync();
    }
    
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
