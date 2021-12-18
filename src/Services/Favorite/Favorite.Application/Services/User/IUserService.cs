using System;

namespace Favorite.Application.Services.User
{
    public interface IUserService
    {
        Guid GetUserId();
        string GetGivenName();
    }
}