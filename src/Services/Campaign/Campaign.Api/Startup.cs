using Campaign.Api.GrpcServices;
using Campaign.Application;
using Campaign.Infrastructure;
using Joker.CAP;
using Joker.Consul;
using Joker.Mongo;
using Joker.Mongo.Domain;
using Joker.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Campaign.Api
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
            services.AddGrpc();
            services.AddMongo(x => Configuration.GetSection("Mongo").Bind(x));
            services.AddMongoContext<CampaignContext>();
            services.AddMongoDomainRepositories();
            services.AddApplicationModule();
            services.AddJokerMediatr(typeof(CampaignApplicationModule));
            services.AddSwaggerGen();
            services.AddJokerCAP(capOptions =>
            {
                capOptions.UseRabbitMQ(x =>
                {
                    x.Password = Configuration["rabbitMQSettings:password"];
                    x.UserName = Configuration["rabbitMQSettings:username"];
                    x.HostName = Configuration["rabbitMQSettings:host"];
                    x.Port = int.Parse(Configuration["rabbitMQSettings:port"]);
                });

                capOptions.UseMongoDB(opt => // Persistence
                {
                    opt.DatabaseConnection = Configuration["mongo:ConnectionString"];
                    opt.DatabaseName = Configuration["mongo:DefaultDatabaseName"] + "EventHistories";
                    opt.PublishedCollection = "PublishedEvents";
                    opt.ReceivedCollection = "ReceivedEvents";
                });

                capOptions.UseDashboard();
                capOptions.FailedRetryCount = 3;
                capOptions.FailedRetryInterval = 60;
            });

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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Campaign.Api v1"));
            app.UseErrorHandler();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapGrpcService<CampaignGrpcService>();
            });
        }
    }
}