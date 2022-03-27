using Elastic.Apm.AspNetCore;
using Joker.Configuration;
using Joker.EntityFrameworkCore.Migration;
using Joker.Identity.Extensions;
using Joker.Identity.Models;
using Joker.Identity.Models.Seeders;
using Joker.Logging;
using Serilog;

var configuration = JokerConfigurationHelper.GetConfiguration();
Log.Logger = LoggerBuilder.CreateLoggerElasticSearch(x =>
{
    x.Url = configuration["elk:url"];
    x.BasicAuthEnabled = false;
    x.IndexFormat = "joker-logs";
    x.AppName = "Identity.Api";
    x.Enabled = true;
    x.Configuration = configuration;
    x.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
});


try
{
    var builder = WebApplication.CreateBuilder(args);

    var services = builder.Services;

    services.AddControllersWithViews().AddRazorRuntimeCompilation();
    services.AddJokerIdentity();
    services.AddHttpContextAccessor();
    services.AddJokerIdentityContext(configuration);
    services.AddJokerIdentityServer(configuration);
    services.AddJokerAuthentication();
    services.AddJokerAuthorization();
    services.AddDataProtection();
    services.AddJokerCors();
    services.AddJokerEventBus(configuration);
    services.AddJokerOpenTelemetry(configuration);

    builder.Host.UseSerilog();
    
    var app = builder.Build();

    app.MigrateDbContext<JokerIdentityDbContext>((context, serviceProvider) =>
    {
        new JokerIdentityDbContextSeeder().SeedAsync(x =>
        {
            var logger = serviceProvider.GetService<ILogger<JokerIdentityDbContext>>();
            x.Context = context;
            x.Logger = logger;
            x.RetryCount = 5;
        }, serviceProvider).Wait();
    });
    
    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();
    
    app.UseElasticApm(configuration);
    app.UseStaticFiles();
    app.UseForwardedHeaders();
    app.UseIdentityServer();
    app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
    app.UseCors("CorsPolicy");
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
    
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

