using System;

namespace Favorite.Application.Services
{
    public interface IUserService
    {
        Guid GetUserId();
        string GetGivenName();
    }
}