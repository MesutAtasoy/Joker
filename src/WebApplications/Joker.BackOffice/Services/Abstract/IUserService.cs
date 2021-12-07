namespace Joker.BackOffice.Services.Abstract;

public interface IUserService
{
    Guid GetOrganizationId();
    string GetOrganizationName();
    Guid GetUserId();
}