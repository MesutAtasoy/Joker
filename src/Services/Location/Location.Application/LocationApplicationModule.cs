using Location.Core.Repositories;
using Location.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Location.Application
{
    public static class LocationApplicationModule
    {
        public static IServiceCollection AddApplicationModule(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<INeighborhoodRepository, NeighborhoodRepository>();
            services.AddScoped<IQuarterRepository, QuarterRepository>();

            return services;
        }
    }
}