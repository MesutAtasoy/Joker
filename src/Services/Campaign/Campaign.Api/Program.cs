using Campaign.Api.Extensions;
using Campaign.Api.GrpcServices;
using Campaign.Api.GrpcServices.MappingProfiles;
using Campaign.Application;
using Elastic.Apm.AspNetCore;
using Joker.Configuration;
using Joker.Logging;
using Joker.Mvc;
using Serilog;

var configuration = JokerConfigurationHelper.GetConfiguration();

Log.Logger = LoggerBuilder.CreateLoggerElasticSearch(x =>
{
    x.Url = configuration["elk:url"];
    x.BasicAuthEnabled = false;
    x.IndexFormat = "joker-logs";
    x.AppName = "Campaign.Api";
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
    services.AddControllers();
    services.AddJokerGrpc();
    services.AddHttpContextAccessor();
    services.AddJokerMongo(configuration);
    services.AddApplicationModule();
    services.AddJokerMediatr(typeof(CampaignApplicationModule));
    services.AddSwaggerGen();
    services.AddJokerEventBus(configuration);
    services.AddJokerConsul(configuration);
    services.AddJokerAuthorization();
    services.AddJokerAuthentication(configuration);
    services.AddJokerOpenTelemetry(configuration);
    services.AddAutoMapper(typeof(CampaignMappingProfile));

    builder.Host.UseSerilog();
    
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Campaign.Api v1"));
    app.UseElasticApm(configuration);
    app.UseErrorHandler();
    app.UseRouting();
    app.UseAuthentication();    
    app.UseAuthorization();       
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapControllers();
        endpoints.MapGrpcService<CampaignGrpcService>();
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
