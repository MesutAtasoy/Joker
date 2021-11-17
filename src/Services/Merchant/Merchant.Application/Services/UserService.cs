using Joker.Extensions;
using Microsoft.AspNetCore.Http;

namespace Merchant.Application.Services;

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
}