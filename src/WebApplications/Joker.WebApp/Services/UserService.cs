using System.Security.Claims;
using Joker.Extensions;
using Joker.WebApp.Services.Abstract;

namespace Joker.WebApp.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _accessor;

    public UserService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
        
    public Guid GetUserId()
    {
        return _accessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value.ToGuid() ?? Guid.Empty;
    }
}