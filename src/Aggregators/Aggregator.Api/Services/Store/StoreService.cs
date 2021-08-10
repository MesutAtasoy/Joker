using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Store;
using Grpc.Core;
using Joker.Extensions;
using Joker.Response;
using Merchant.Api.Grpc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Aggregator.Api.Services.Store
{
    public class StoreService : IStoreService
    {
        private readonly MerchantApiGrpcService.MerchantApiGrpcServiceClient _merchantApiGrpcServiceClient;
        private readonly IHttpContextAccessor _contextAccessor;
        
        public StoreService(MerchantApiGrpcService.MerchantApiGrpcServiceClient merchantApiGrpcServiceClient, IHttpContextAccessor contextAccessor)
        {
            _merchantApiGrpcServiceClient = merchantApiGrpcServiceClient;
            _contextAccessor = contextAccessor;
            _contextAccessor = contextAccessor;
        }

        public async Task<JokerBaseResponse<StoreModel>> CreateAsync(CreateStoreModel request)
        {
            var headers = await GetHeaders();
            
            var response = await _merchantApiGrpcServiceClient.CreateStoreAsync(new CreateStoreMessage
            {
                Email = request.Email ?? "",
                Description = request.Description?? "",
                Name = request.Name?? "",
                Slogan = request.Slogan?? "",
                MerchantId = request.MerchantId.ToString(),
                PhoneNumber = request.PhoneNumber?? "",
                Location = new StoreLocationMessage
                {
                    Country = new IdName
                    {
                        Id = request.Location.Country.Id.ToString(),
                        Name = request.Location.Country.Name
                    },
                    City = new IdName
                    {
                        Id = request.Location.City.Id.ToString(),
                        Name = request.Location.City.Name
                    },
                    District = new IdName
                    {
                        Id = request.Location.District.Id.ToString(),
                        Name = request.Location.District.Name
                    },
                    Neighborhood = new IdName
                    {
                        Id = request.Location.Neighborhood.Id.ToString(),
                        Name = request.Location.Neighborhood.Name
                    },
                    Quarter = new IdName
                    {
                        Id = request.Location.Quarter.Id.ToString(),
                        Name = request.Location.Quarter.Name
                    },
                    Address = request.Location.Address?? ""
                }
            }, headers);
            
            if (response.Status != 200)
            {
                return new JokerBaseResponse<StoreModel>(null, response.Status, response.Message);
            }
            
            var store = response.Data.Unpack<StoreMessage>();
            return new JokerBaseResponse<StoreModel>(As(store), 200);
        }

        public async Task<JokerBaseResponse<StoreModel>> UpdateAsync(UpdateStoreModel request)
        {
            var headers = await GetHeaders();

            
            var response = await _merchantApiGrpcServiceClient.UpdateStoreAsync(new UpdateStoreMessage
            {
                StoreId = request.Id.ToString(),
                Store = new UpdateStoreItemMessage
                {
                    Email = request.Email,
                    Description = request.Description,
                    Name = request.Name,
                    Slogan = request.Slogan,
                    PhoneNumber = request.PhoneNumber
                }
            }, headers);
            
            if (response.Status != 200)
            {
                return new JokerBaseResponse<StoreModel>(null, response.Status, response.Message);
            }
            
            var store = response.Data.Unpack<StoreMessage>();
            return new JokerBaseResponse<StoreModel>(As(store), 200);
        }
        
        public async Task<JokerBaseResponse<StoreLocationModel>> UpdateLocationAsync(UpdateStoreLocationModel request)
        {
            var headers = await GetHeaders();

            var response = await _merchantApiGrpcServiceClient.UpdateLocationAsync(new UpdateStoreLocationMessage
            {
                StoreId = request.Id.ToString(),
                Location = new StoreLocationMessage
                {
                    Country = new IdName
                    {
                        Id = request.Location.Country.Id.ToString(),
                        Name = request.Location.Country.Name
                    },
                    City = new IdName
                    {
                        Id = request.Location.City.Id.ToString(),
                        Name = request.Location.City.Name
                    },
                    District = new IdName
                    {
                        Id = request.Location.District.Id.ToString(),
                        Name = request.Location.District.Name
                    },
                    Neighborhood = new IdName
                    {
                        Id = request.Location.Neighborhood.Id.ToString(),
                        Name = request.Location.Neighborhood.Name
                    },
                    Quarter = new IdName
                    {
                        Id = request.Location.Quarter.Id.ToString(),
                        Name = request.Location.Quarter.Name
                    },
                    Address = request.Location.Address
                }
            }, headers);

            if (response.Status != 200)
            {
                return new JokerBaseResponse<StoreLocationModel>(null, response.Status, response.Message);
            }
            
            var storeLocationModel =  new StoreLocationModel
            {
                Country = new Models.Shared.IdName()
                {
                    Id = request.Location.Country.Id,
                    Name = request.Location.Country.Name
                },
                City = new Models.Shared.IdName
                {
                    Id = request.Location.City.Id,
                    Name = request.Location.City.Name
                },
                District = new Models.Shared.IdName
                {
                    Id = request.Location.District.Id,
                    Name = request.Location.District.Name
                },
                Neighborhood = new Models.Shared.IdName
                {
                    Id = request.Location.Neighborhood.Id,
                    Name = request.Location.Neighborhood.Name
                },
                Quarter = new Models.Shared.IdName
                {
                    Id = request.Location.Quarter.Id,
                    Name = request.Location.Quarter.Name
                },
                Address = request.Location.Address
            };
            
            var storeLocation = response.Data.Unpack<StoreLocationMessage>();
            return new JokerBaseResponse<StoreLocationModel>(storeLocationModel, 200);
        }

        public async Task<JokerBaseResponse<bool>> DeleteAsync(Guid id)
        {
            var headers = await GetHeaders();

            var response =  await _merchantApiGrpcServiceClient.DeleteStoreAsync(new ByIdMessage {Id = id.ToString()}, headers);
            
            if (response.Status != 200)
            {
                return new JokerBaseResponse<bool>(false, response.Status, response.Message);
            }
            
            var deleteCampaignMessage = response.Data.Unpack<DeleteStoreResponseMessage>();
            return new JokerBaseResponse<bool>(deleteCampaignMessage.IsSucceed, 200);
        }

        public async Task<StoreModel> GetByIdAsync(Guid id)
        {
            var headers = await GetHeaders();
            var store =await _merchantApiGrpcServiceClient.GetStoreByIdAsync(new ByIdMessage {Id = id.ToString()}, headers);
            return As(store);
        }

        private async Task<Metadata> GetHeaders()
        {
            var accessToken = await _contextAccessor?.HttpContext?.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var headers = new Metadata();
            if (!string.IsNullOrEmpty(accessToken))
            {
                headers.Add("Authorization", $"Bearer {accessToken}");
            }

            return headers;
        }
        
        #region Model Converters

        private StoreModel As(StoreMessage  storeMessage)
        {
            return new()
            {
                Id = storeMessage.Id.ToGuid(),
                Name = storeMessage.Name,
                Description = storeMessage.Description,
                Email = storeMessage.Email,
                Merchant = new Models.Shared.IdName
                {
                    Id = storeMessage.Merchant.Id.ToGuid(),
                    Name = storeMessage.Merchant.Name
                },
                Location = new StoreLocationModel
                {
                    Country = new Models.Shared.IdName
                    {
                        Id = storeMessage.Location.Country.Id.ToGuid(),
                        Name = storeMessage.Location.Country.Name
                    },
                    City = new Models.Shared.IdName
                    {
                        Id = storeMessage.Location.City.Id.ToGuid(),
                        Name = storeMessage.Location.City.Name
                    },
                    District = new Models.Shared.IdName
                    {
                        Id = storeMessage.Location.District.Id.ToGuid(),
                        Name = storeMessage.Location.District.Name
                    },
                    Neighborhood = new Models.Shared.IdName
                    {
                        Id = storeMessage.Location.Neighborhood.Id.ToGuid(),
                        Name = storeMessage.Location.Neighborhood.Name
                    },
                    Quarter = new Models.Shared.IdName
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
}