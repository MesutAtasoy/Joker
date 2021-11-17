using System.Globalization;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Joker.WebApp.HttpHandlers;

public class BearerTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory;

    public BearerTokenHandler(IHttpContextAccessor httpContextAccessor,
        IHttpClientFactory httpClientFactory)
    {
        _httpContextAccessor = httpContextAccessor ??
                               throw new ArgumentNullException(nameof(httpContextAccessor));
        _httpClientFactory = httpClientFactory ??
                             throw new ArgumentNullException(nameof(httpClientFactory));
    }
        
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        var accessToken = await GetAccessTokenAsync();

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.SetBearerToken(accessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }

    public async Task<string> GetAccessTokenAsync()
    {
        // get the expires_at value & parse it
        var expiresAt = await _httpContextAccessor.HttpContext.GetTokenAsync("expires_at");

        if (string.IsNullOrEmpty(expiresAt))
        {
            return null;
        }

        var expiresAtAsDateTimeOffset = DateTimeOffset.Parse(expiresAt, CultureInfo.InvariantCulture);

        if ((expiresAtAsDateTimeOffset.AddSeconds(-60)).ToUniversalTime() > DateTime.UtcNow)
        {
            // no need to refresh, return the access token
            return await _httpContextAccessor
                .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        }

        var idpClient = _httpClientFactory.CreateClient("IdentityApi");

            
        // get the discovery document
        var discoveryResponse = await idpClient.GetDiscoveryDocumentAsync();

            
        // refresh the tokens
        var refreshToken = await _httpContextAccessor
            .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

        if (string.IsNullOrEmpty(refreshToken))
        {
            return null;
        }

        var refreshResponse = await idpClient.RequestRefreshTokenAsync(
            new RefreshTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = "joker.web.app",
                ClientSecret = "secret",
                RefreshToken = refreshToken
            });

        // store the tokens             
        var updatedTokens = new List<AuthenticationToken>
        {
            new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.IdToken, Value = refreshResponse.IdentityToken
            },
            new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.AccessToken, Value = refreshResponse.AccessToken
            },
            new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.RefreshToken, Value = refreshResponse.RefreshToken
            },
            new AuthenticationToken
            {
                Name = "expires_at",
                Value = (DateTime.UtcNow + TimeSpan.FromSeconds(refreshResponse.ExpiresIn)).ToString("o",
                    CultureInfo.InvariantCulture)
            }
        };

        // get authenticate result, containing the current principal & 
        // properties
        var currentAuthenticateResult = await _httpContextAccessor
            .HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // store the updated tokens
        currentAuthenticateResult.Properties.StoreTokens(updatedTokens);

        // sign in
        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            currentAuthenticateResult.Principal,
            currentAuthenticateResult.Properties);

        return refreshResponse.AccessToken;
    }
}