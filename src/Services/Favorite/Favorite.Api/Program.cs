using Favorite.Api.Extensions;
using Favorite.Api.GrpcServices;
using Favorite.Application;
using Joker.Configuration;
using Joker.Logging;
using Joker.Mvc;
using Joker.Mvc.Initializers;
using Serilog;

var configuration = JokerConfigurationHelper.GetConfiguration();
Log.Logger = LoggerBuilder.CreateLoggerElasticSearch(x =>
{
    x.Url = configuration["elk:url"];
    x.BasicAuthEnabled = false;
    x.IndexFormat = "joker-logs";
    x.AppName = "Favorite.Api";
    x.Enabled = true;
});

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.WebHost.BuildKestrel(configuration);

    var services = builder.Services;
    services.AddControllers();
    services.AddJokerGrpc();
    services.AddHttpContextAccessor();
    services.AddApplicationModule();
    services.AddHttpClient();
    services.AddJokerMediatr(typeof(FavoriteApplicationModule));
    services.AddSwaggerGen();
    services.AddJokerEventBus(configuration);
    services.AddJokerCouchbase(configuration);
    services.AddCouchbaseInitializers();
    services.AddJokerConsul(configuration);
    services.AddJokerAuthentication(configuration);
    services.AddJokerAuthorization();
    services.AddJokerOpenTelemetry(configuration);

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Favorite.Api v1"));
    app.UseErrorHandler();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapControllers();
        endpoints.MapGrpcService<FavoriteGrpcService>();
    });

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