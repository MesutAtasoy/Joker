using Aggregator.StoreFront.Api.Extensions;
using Aggregator.StoreFront.Api.Models.Campaign.MappingProfiles;
using AutoMapper;
using Joker.Mvc;

namespace Aggregator.StoreFront.Api;

public class Startup
{
    private readonly IConfiguration _configuration;
        
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddGrpcServices(_configuration);
        services.AddApiVersion();
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddGrpc();
        services.AddSwaggerGen();
        services.AddJokerIdentityApiClient(_configuration);
        services.AddJokerAuthentication(_configuration);
        services.AddJokerAuthorization();
        services.AddJokerConsul(_configuration);
        services.AddJokerOpenTelemetry(_configuration);
        services.AddAutoMapper(typeof(CampaignMappingProfile));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aggregator.StoreFront.Api v1"));
        app.UseErrorHandler();
        app.UseRouting();
        app.UseAuthentication();    
        app.UseAuthorization();        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
        });
    }
}