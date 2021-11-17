using Location.Infrastructure.Seed.Seeders.Base;
using Microsoft.Extensions.Logging;

namespace Location.Infrastructure.Seed;

public class LocationContextSeeder
{
    public async Task SeedAsync(Action<LocationContextSeederOptions> action, IServiceProvider serviceProvider)
    {
        var options = new LocationContextSeederOptions();
        action.Invoke(options);
        CheckNull(options);

        try
        {
            var context = options.Context;
            var seeders = GetClassByType<ISeeder>();
            var tasks = seeders.OrderBy(o => o.Order).ToList();
            foreach (var seeder in tasks)
            {
                await seeder.SeedAsync(context, options.ContentRootPath, options.Logger, serviceProvider);
            }
                
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            options.Logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(LocationContext));
            await SeedAsync(action, serviceProvider);
        }
    }


        

    #region Utilies Functions

    public void CheckNull(LocationContextSeederOptions options)
    {
        if (string.IsNullOrEmpty(options.ContentRootPath))
            throw new ArgumentNullException(options.ContentRootPath);
        if (options.Context == null)
            throw new ArgumentNullException(nameof(options.Context));
        if (options.RetryCount == default)
            options.RetryCount = 5;
    }

    public IList<T> GetClassByType<T>()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(T).IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface)
            .Select(c => (T) Activator.CreateInstance(c))
            .ToList();
    }

    #endregion
}