using AutoMapper;
using Joker.Configuration;
using Joker.Logging;
using Joker.Mvc;
using Merchant.Api.Extensions;
using Merchant.Api.GrpcServices;
using Merchant.Api.GrpcServices.MappingProfiles;
using Merchant.Application;
using Merchant.Domain;
using Serilog;

var configuration = JokerConfigurationHelper.GetConfiguration();
Log.Logger = LoggerBuilder.CreateLoggerElasticSearch(x =>
{
    x.Url = configuration["elk:url"];
    x.BasicAuthEnabled = false;
    x.IndexFormat = "joker-logs";
    x.AppName = "Merchant.Api";
    x.Enabled = true;
});

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    builder.WebHost.BuildKestrel(configuration);
    
    var services = builder.Services;
    services.AddApiVersion();
    services.AddJokerGrpc();
    services.AddControllers();
    services.AddHttpContextAccessor();
    services.AddJokerMongo(configuration);
    services.AddApplicationModule();
    services.AddDomainModule();
    services.AddHttpClient();
    services.AddJokerMediatr(typeof(MerchantApplicationModule));
    services.AddSwaggerGen();
    services.AddJokerEventBus(configuration);
    services.AddJokerConsul(configuration);
    services.AddJokerAuthorization();
    services.AddJokerAuthentication(configuration);
    services.AddJokerOpenTelemetry(configuration);
    services.AddAutoMapper(typeof(StoreMappingProfile));
    
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Merchant.Api v1"));
    app.UseErrorHandler();
    app.UseRouting();
    app.UseAuthentication();    
    app.UseAuthorization();        
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapControllers();
        endpoints.MapGrpcService<MerchantGrpcService>();
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
