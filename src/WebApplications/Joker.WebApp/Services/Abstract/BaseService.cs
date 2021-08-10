using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Joker.WebApp.Services.Abstract
{
    public abstract class BaseService
    {
        public virtual async Task<T> HandleRequestAsync<T>(HttpResponseMessage message)
        {
            try
            {
                var responseViewModel = await message.Content.ReadFromJsonAsync<T>();
                return responseViewModel;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Service can not respond success response", e);
            }
        }
    }
}