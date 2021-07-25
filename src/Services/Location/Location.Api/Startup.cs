using Joker.Consul;
using Joker.EntityFrameworkCore;
using Joker.Mvc;
using Location.Api.Extensions;
using Location.Api.GrpcServices;
using Location.Api.Interceptors;
using Location.Application;
using Location.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Location.Api
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
            services.AddJokerGrpc();
            services.AddControllers();
            services.AddJokerContext(Configuration);
            services.AddApplicationModule();
            services.AddJokerMediatr(typeof(LocationApplicationModule));
            services.AddSwaggerGen();
            services.AddJokerConsul(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Location.Api v1"));
            app.UseErrorHandler();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapGrpcService<LocationGrpcService>();
            });
        }
    }
}