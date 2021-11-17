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

    public Guid GetOrganizationId()
    {
        var organizationId =  _accessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "organizationId")?.Value;
        return organizationId == null ? Guid.Empty : Guid.Parse(organizationId);
    }

    public string GetOrganizationName()
    {
        return _accessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == "organizationName")?.Value;
    }
        
    public Guid GetUserId()
    {
        return _accessor?.HttpContext?.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value.ToGuid() ?? Guid.Empty;
    }
}