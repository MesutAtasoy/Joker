using Joker.Identity.Models.Seeders.Base;

namespace Joker.Identity.Models.Seeders;

public class JokerIdentityDbContextSeeder
{
    public async Task SeedAsync(Action<JokerIdentityDbContextSeederOptions> action, IServiceProvider serviceProvider)
    {
        var options = new JokerIdentityDbContextSeederOptions();
        action.Invoke(options);
        
        CheckNull(options);
            
        var context = options.Context;
        var seeders = GetClassByType<ISeeder>();
        var tasks = seeders.OrderBy(o => o.Order).ToList();
        
        foreach (var seeder in tasks)
        {
            await seeder.SeedAsync(context, options.Logger, serviceProvider);
        }
    }
    
    #region Utilies Functions

    public void CheckNull(JokerIdentityDbContextSeederOptions options)
    {
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