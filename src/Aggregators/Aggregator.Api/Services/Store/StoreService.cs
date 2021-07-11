using System;
using System.Threading.Tasks;
using Aggregator.Api.Models.Store;
using Joker.Extensions;
using Merchant.Api.Grpc;

namespace Aggregator.Api.Services.Store
{
    public class StoreService : IStoreService
    {
        private readonly MerchantApiGrpcService.MerchantApiGrpcServiceClient _merchantApiGrpcServiceClient;
        
        public StoreService(MerchantApiGrpcService.MerchantApiGrpcServiceClient merchantApiGrpcServiceClient)
        {
            _merchantApiGrpcServiceClient = merchantApiGrpcServiceClient;
        }

        public async Task<StoreModel> CreateAsync(CreateStoreModel request)
        {
            var response = await _merchantApiGrpcServiceClient.CreateStoreAsync(new CreateStoreMessage
            {
                Email = request.Email,
                Description = request.Description,
                Name = request.Name,
                Slogan = request.Slogan,
                MerchantId = request.MerchantId.ToString(),
                PhoneNumber = request.PhoneNumber,
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
            });

            return As(response);
        }

        public async Task<StoreModel> UpdateAsync(UpdateStoreModel request)
        {
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
            });
            
            return As(response);

        }
        
        public async Task<StoreLocationModel> UpdateLocationAsync(UpdateStoreLocationModel request)
        {
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
            });

            return new StoreLocationModel
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
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response =  await _merchantApiGrpcServiceClient.DeleteStoreAsync(new ByIdMessage {Id = id.ToString()});
            return response.IsSucceed;
        }

        public async Task<StoreModel> GetByIdAsync(Guid id)
        {
            var store =await _merchantApiGrpcServiceClient.GetStoreByIdAsync(new ByIdMessage {Id = id.ToString()});
            return As(store);
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