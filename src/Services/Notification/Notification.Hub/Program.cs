using Joker.Configuration;
using Joker.Logging;
using Joker.Mvc;
using Notification.Hub;
using Notification.Hub.Extensions;
using Serilog;

var configuration = JokerConfigurationHelper.GetConfiguration();
Log.Logger = LoggerBuilder.CreateLoggerElasticSearch(x =>
{
    x.Url = configuration["elk:url"];
    x.BasicAuthEnabled = false;
    x.IndexFormat = "joker-logs";
    x.AppName = "Notification.Hub";
    x.Enabled = true;
});

try
{
    var builder = WebApplication.CreateBuilder(args);
    
    var services = builder.Services;
    services.AddApiVersion();
    services.AddControllers();
    services.AddHttpContextAccessor();
    services.AddHttpClient();
    services.AddJokerEventBus(configuration);
    services.AddJokerConsul(configuration);
    services.AddJokerAuthorization();
    services.AddJokerAuthentication(configuration);
    services.AddJokerOpenTelemetry(configuration);
    services.AddSignalR();
    services.AddSwaggerGen();
    services.AddJokerCors();
    
    var app = builder.Build();
    
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();
    
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notification.Hub v1"));
    app.UseErrorHandler();
    app.UseRouting();
    app.UseCors("CorsPolicy");
    app.UseAuthentication();    
    app.UseAuthorization();        
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapControllers();
        endpoints.MapHub<NotificationHub>("/notification");
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
