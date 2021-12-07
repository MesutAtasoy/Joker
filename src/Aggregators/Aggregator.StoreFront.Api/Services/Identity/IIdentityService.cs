using Aggregator.StoreFront.Api.Models.Organization.Requests;

namespace Aggregator.StoreFront.Api.Services.Identity;

public interface IIdentityService
{
    Task<(bool IsSucceed, CreateOrganizationResponse Response)> CreateOrganization(string organizationName);
}