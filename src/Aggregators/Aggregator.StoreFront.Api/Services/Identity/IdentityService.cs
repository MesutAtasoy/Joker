using System.Net.Http.Headers;
using Aggregator.StoreFront.Api.Models.Organization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Aggregator.StoreFront.Api.Services.Identity;

public class IdentityService : IIdentityService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly HttpClient _httpClient;
    private readonly ILogger<IdentityService> _logger;
    private readonly IHttpContextAccessor _contextAccessor;

    public IdentityService(IHttpClientFactory clientFactory, ILogger<IdentityService> logger, IHttpContextAccessor contextAccessor)
    {
        _clientFactory = clientFactory;
        _logger = logger;
        _contextAccessor = contextAccessor;
        _httpClient = _clientFactory.CreateClient("IdentityApi");
    }

    public async Task<(bool IsSucceed, CreateOrganizationResponse Response)> CreateOrganization(string organizationName)
    {
        try
        {
            var response = new CreateOrganizationResponse();
            
            var requestModel = new
            {
                Name = organizationName
            };

            var accessToken = await _contextAccessor?.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.AccessToken)! ??"";
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            var responseMessage = await _httpClient.PostAsJsonAsync("api/Organizations", requestModel);

            if (responseMessage.IsSuccessStatusCode)
            {
                response = await responseMessage.Content.ReadFromJsonAsync<CreateOrganizationResponse>();
            }
   
            return (responseMessage.IsSuccessStatusCode, response);
        }
        catch (Exception e)
        {

            return (false, null);
        }
    }
}