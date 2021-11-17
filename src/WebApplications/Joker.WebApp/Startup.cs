using Joker.WebApp.Extensions;
using Joker.WebApp.Hubs;

namespace Joker.WebApp;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();
        services.AddAuthorization();
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddJokerGatewayApiClient(Configuration);
        services.AddJokerIdentityApiClient(Configuration);
        services.AddJokerAuthentication(Configuration);
        services.AddApiServices();
        services.AddUserServices();
        services.AddJokerEventBus(Configuration);
        services.AddDataProtection();
        services.AddSignalR();
        services.AddJokerOpenTelemetry(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
        app.UseRouting();
        app.UseForwardedHeaders();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapHub<CampaignCreatedNotificationHub>("/campaignCreated");
        });
    }
}