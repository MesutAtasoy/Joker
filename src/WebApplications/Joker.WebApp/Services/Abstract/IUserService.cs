namespace Joker.WebApp.Services.Abstract;

public interface IUserService
{
    Guid GetOrganizationId();
    string GetOrganizationName();
    Guid GetUserId();
}