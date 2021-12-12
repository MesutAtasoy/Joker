using System;
using System.Linq;
using Joker.Extensions;
using Microsoft.AspNetCore.Http;

namespace Favorite.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        
        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        
        public Guid GetUserId()
        {
            return _contextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "sub")?.Value.ToGuid() ?? Guid.Empty;
        }

        public string GetGivenName()
        {
            return _contextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "given_name")?.Value ?? string.Empty;
        }
    }
}