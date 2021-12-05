namespace Campaign.Application.Services;

public interface IUserService
{
    Guid GetUserId();
    Guid GetOrganizationId();
}