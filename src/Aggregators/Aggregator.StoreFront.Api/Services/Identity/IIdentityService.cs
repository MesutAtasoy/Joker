using Aggregator.StoreFront.Api.Models.Organization;

namespace Aggregator.StoreFront.Api.Services.Identity;

public interface IIdentityService
{
    Task<(bool IsSucceed, CreateOrganizationResponse Response)> CreateOrganization(string organizationName);
}