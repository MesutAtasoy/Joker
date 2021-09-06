using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Joker.WebApp.ViewModels.Shared;

namespace Joker.WebApp.Services.Abstract
{
    public abstract class BaseService
    {
        protected virtual async Task<JokerBaseResponseViewModel<T>> HandleRequestAsync<T>(HttpResponseMessage message)
        {
            try
            {
                var responseViewModel = await message.Content.ReadFromJsonAsync<JokerBaseResponseViewModel<T>>();
                return responseViewModel;
            }
            catch (Exception e)
            {
                return new JokerBaseResponseViewModel<T>
                {
                    Message = e.Message,
                    Payload = default,
                    StatusCode = (int)message.StatusCode
                };
            }
        }
    }
}