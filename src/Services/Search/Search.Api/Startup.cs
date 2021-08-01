using Joker.Mvc;
using Joker.Mvc.Initializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Search.Api.Extensions;
using Search.Application;
using Search.Core;

namespace Search.Api
{
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
            services.AddApplicationModule();
            services.AddCoreModule();
            services.AddJokerMediatr(typeof(SearchApplicationModule));
            services.AddSwaggerGen();
            services.AddElasticService(Configuration);
            services.AddJokerEventBus(Configuration);
            services.AddJokerConsul(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            IStartupInitializer initializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Search.Api v1"));
            app.UseErrorHandler();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            initializer.InitializeAsync().Wait();
        }
    }
}