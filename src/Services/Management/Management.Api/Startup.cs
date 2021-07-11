using AutoMapper;
using Joker.Consul;
using Joker.EntityFrameworkCore;
using Joker.Mvc;
using Management.Api.GrpcServices;
using Management.Api.Interceptors;
using Management.Application;
using Management.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Management.Api
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
            services.AddGrpc(x => x.Interceptors.Add<GrpcExceptionInterceptor>());
            services.AddControllers();
            services.AddJokerNpDbContext<ManagementContext>(x =>
            {
                x.ConnectionString = Configuration["connectionString"];
                x.EnableMigration = true;
                x.MaxRetryCount = 3;
            });
            
            services.AddApplicationModule();
            services.AddAutoMapper(typeof(ManagementGrpcMappingProfile));
            services.AddJokerMediatr(typeof(ManagementApplicationModule));
            services.AddSwaggerGen();
            services.RegisterConsulServices(x => Configuration.GetSection("ServiceDiscovery").Bind(x));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Management.Api v1"));
            app.UseErrorHandler();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                // endpoints.MapGrpcService<ManagementGrpcService>();
            });
        }
    }
}