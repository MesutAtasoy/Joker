using Joker.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Subscription.Api.Extensions;
using Subscription.Application;

namespace Subscription.Api
{
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
            services.AddApiVersion();
            services.AddControllers();
            services.AddJokerMongo(Configuration);
            services.AddApplicationModule();
            services.AddHttpClient();
            services.AddJokerMediatr(typeof(SubscriptionApplicationModule));
            services.AddSwaggerGen();
            services.AddJokerEventBus(Configuration);
            services.AddJokerConsul(Configuration);
            services.AddAuthorization();
            services.AddJokerAuthentication(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
            });
        }
    }
}