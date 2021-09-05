using Couchbase.Extensions.DependencyInjection;
using Favorite.Api.Extensions;
using Favorite.Application;
using Favorite.Infrastructure.Initializers;
using Joker.Mvc;
using Joker.Mvc.Initializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Favorite.Api
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
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddApplicationModule();
            services.AddHttpClient();
            services.AddJokerMediatr(typeof(FavoriteApplicationModule));
            services.AddSwaggerGen();
            services.AddJokerEventBus(Configuration);
            services.AddJokerCouchbase(Configuration);
            services.AddCouchbaseInitializers();
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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Favorite.Api v1"));
            app.UseErrorHandler();
            app.UseRouting();
            app.UseAuthentication();    
            app.UseAuthorization();        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });

            initializer.InitializeAsync().Wait();
        }
    }
}