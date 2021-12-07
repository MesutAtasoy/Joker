namespace Merchant.Application.Services;

public interface IUserService
{
    Guid GetUserId();
    Guid GetOrganizationId();
}