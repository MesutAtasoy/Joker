using Joker.Mvc;
using Notification.Hub.Extensions;

namespace Notification.Hub;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApiVersion();
        services.AddControllers();
        services.AddHttpContextAccessor();
        services.AddHttpClient();
        services.AddJokerEventBus(Configuration);
        services.AddJokerConsul(Configuration);
        services.AddJokerAuthorization();
        services.AddJokerAuthentication(Configuration);
        services.AddJokerOpenTelemetry(Configuration);
        services.AddSignalR();
        services.AddSwaggerGen();
        services.AddJokerCors();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

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
    }
}