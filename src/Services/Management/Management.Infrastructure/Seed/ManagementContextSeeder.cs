using Management.Infrastructure.Seed.Base;
using Microsoft.Extensions.Logging;

namespace Management.Infrastructure.Seed;

public class ManagementContextSeeder
{
    public async Task SeedAsync(Action<ManagementContextSeederOptions> action, IServiceProvider serviceProvider)
    {
        var options = new ManagementContextSeederOptions();
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
            options.Logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(ManagementContext));
            await SeedAsync(action, serviceProvider);
        }
    }


        

    #region Utilies Functions

    public void CheckNull(ManagementContextSeederOptions options)
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