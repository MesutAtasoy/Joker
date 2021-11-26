using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services.Abstract;

public abstract class BaseService
{
    private readonly ILogger _logger;

    protected BaseService(ILogger logger)
    {
        _logger = logger;
    }

    protected virtual async Task<JokerBaseResponseViewModel<T>> HandleRequestAsync<T>(HttpResponseMessage message)
    {
        try
        {
            _logger.LogError(await message.Content.ReadAsStringAsync());
            _logger.LogError(message.StatusCode.ToString());
            var responseViewModel = await message.Content.ReadFromJsonAsync<JokerBaseResponseViewModel<T>>();
            return responseViewModel;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e.StackTrace);
            
            return new JokerBaseResponseViewModel<T>
            {
                Message = e.Message,
                Payload = default,
                StatusCode = (int)message.StatusCode
            };
        }
    }
}