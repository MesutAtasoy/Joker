using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Aggregator.StoreFront.Api.Services.BaseGrpc;

public class BaseGrpcProvider : IBaseGrpcProvider
{
    private readonly IHttpContextAccessor _accessor;
    
    public BaseGrpcProvider(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    
    
    public async Task<Metadata> GetDefaultHeadersAsync()
    {
        var accessToken = await _accessor?.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        var headers = new Metadata();
        if (!string.IsNullOrEmpty(accessToken))
        {
            headers.Add("Authorization", $"Bearer {accessToken}");
        }

        return headers;
    }
}