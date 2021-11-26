using Aggregator.StoreFront.Api.Models.Store;
using Grpc.Core;
using Joker.Extensions;
using Joker.Response;
using Merchant.Api.Grpc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using StoreModel = Aggregator.StoreFront.Api.Models.Favorite.StoreModel;

namespace Aggregator.StoreFront.Api.Services.Store;

public class StoreService : IStoreService
{
    private readonly MerchantApiGrpcService.MerchantApiGrpcServiceClient _merchantApiGrpcServiceClient;
    private readonly IHttpContextAccessor _contextAccessor;

    public StoreService(MerchantApiGrpcService.MerchantApiGrpcServiceClient merchantApiGrpcServiceClient,
        IHttpContextAccessor contextAccessor)
    {
        _merchantApiGrpcServiceClient = merchantApiGrpcServiceClient;
        _contextAccessor = contextAccessor;
        _contextAccessor = contextAccessor;
    }

    public async Task<Models.Store.StoreModel> GetByIdAsync(Guid id)
    {
        var headers = await GetHeaders();
        var store = await _merchantApiGrpcServiceClient.GetStoreByIdAsync(new ByIdMessage { Id = id.ToString() },
            headers);
        return As(store);
    }

    private async Task<Metadata> GetHeaders()
    {
        var accessToken =
            await _contextAccessor?.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

        var headers = new Metadata();
        if (!string.IsNullOrEmpty(accessToken))
        {
            headers.Add("Authorization", $"Bearer {accessToken}");
        }

        return headers;
    }

    #region Model Converters

    private Models.Store.StoreModel As(StoreMessage storeMessage)
    {
        return new()
        {
            Id = storeMessage.Id.ToGuid(),
            Name = storeMessage.Name,
            Description = storeMessage.Description,
            Email = storeMessage.Email,
            Merchant = new Models.Shared.IdNameModel
            {
                Id = storeMessage.Merchant.Id.ToGuid(),
                Name = storeMessage.Merchant.Name
            },
            Location = new StoreLocationModel
            {
                Country = new Models.Shared.IdNameModel
                {
                    Id = storeMessage.Location.Country.Id.ToGuid(),
                    Name = storeMessage.Location.Country.Name
                },
                City = new Models.Shared.IdNameModel
                {
                    Id = storeMessage.Location.City.Id.ToGuid(),
                    Name = storeMessage.Location.City.Name
                },
                District = new Models.Shared.IdNameModel
                {
                    Id = storeMessage.Location.District.Id.ToGuid(),
                    Name = storeMessage.Location.District.Name
                },
                Neighborhood = new Models.Shared.IdNameModel
                {
                    Id = storeMessage.Location.Neighborhood.Id.ToGuid(),
                    Name = storeMessage.Location.Neighborhood.Name
                },
                Quarter = new Models.Shared.IdNameModel
                {
                    Id = storeMessage.Location.Quarter.Id.ToGuid(),
                    Name = storeMessage.Location.Quarter.Name
                },
                Address = storeMessage.Location.Address
            }
        };
    }

    #endregion
}