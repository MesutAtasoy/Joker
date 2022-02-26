using Joker.Configuration;
using Joker.Logging;
using Joker.Mvc;
using Notification.Api.Extensions;
using Notification.Application;
using Serilog;

var configuration = JokerConfigurationHelper.GetConfiguration();
Log.Logger = LoggerBuilder.CreateLoggerElasticSearch(x =>
{
    x.Url = configuration["elk:url"];
    x.BasicAuthEnabled = false;
    x.IndexFormat = "joker-logs";
    x.AppName = "Notification.Api";
    x.Enabled = true;
});

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    var services = builder.Services;
    services.AddApiVersion();
    services.AddControllers();
    services.AddHttpContextAccessor();
    services.AddJokerMongo(configuration);
    services.AddApplicationModule();
    services.AddHttpClient();
    services.AddJokerMediatr(typeof(NotificationApplicationModule));
    services.AddSwaggerGen();
    services.AddJokerEventBus(configuration);
    services.AddJokerConsul(configuration);
    services.AddJokerAuthorization();
    services.AddJokerAuthentication(configuration);
    services.AddJokerOpenTelemetry(configuration);
    
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification.Api v1"));
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
