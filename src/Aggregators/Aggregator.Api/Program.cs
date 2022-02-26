using Aggregator.Api.Extensions;
using Aggregator.Api.Models.Campaign.MappingProfiles;
using AutoMapper;
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
    x.AppName = "Aggregator.Api";
    x.Enabled = true;
});

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    builder.WebHost.BuildKestrel(configuration);
    
    var services = builder.Services;
    services.AddGrpcServices(configuration);
    services.AddApiVersion();
    services.AddHttpContextAccessor();
    services.AddControllers();
    services.AddGrpc();
    services.AddSwaggerGen();
    services.AddAutoMapper(typeof(CampaignMappingProfile));
    services.AddJokerAuthentication(configuration);
    services.AddJokerAuthorization();
    services.AddJokerConsul(configuration);
    services.AddJokerOpenTelemetry(configuration);
    
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggregator.Api v1"));
    app.UseErrorHandler();
    app.UseRouting();
    app.UseAuthentication();    
    app.UseAuthorization();        
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapControllers();
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