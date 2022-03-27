using Elastic.Apm.AspNetCore;
using Gateway.BackOffice.Api.Extensions;
using Joker.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Serilog;
using ConfigurationExtensions = Gateway.BackOffice.Api.Extensions.ConfigurationExtensions;

var configuration = ConfigurationExtensions.GetConfiguration();

Log.Logger = LoggerBuilder.CreateLoggerElasticSearch(x =>
{
    x.Url = configuration["elk:url"];
    x.BasicAuthEnabled = false;
    x.IndexFormat = "joker-logs";
    x.AppName = "Gateway.BackOffice.Api";
    x.Enabled = true;
    x.Configuration = configuration;
    x.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
});

try
{
    var builder = WebApplication.CreateBuilder(args);

    var services = builder.Services;

    services.AddControllers();

    services.AddOcelot(configuration)
        .AddConsul()
        .AddPolly();

    services.AddCors(options =>
        options.AddDefaultPolicy(policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
    services.AddSwaggerForOcelot(configuration);
    services.AddJokerOpenTelemetry(configuration);

    builder.Host.UseSerilog();
    
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseElasticApm(configuration);
    app.UseCors();
    app.UseSwaggerForOcelotUI();
    app.UseRouting();
    app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    app.UseOcelot().Wait();

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