using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Joker.Extensions;
using Merchant.Api.Grpc;
using Merchant.Application.Merchants;
using Merchant.Application.Merchants.Commands.CreateMerchant;
using Merchant.Application.Merchants.Commands.DeleteMerchant;
using Merchant.Application.Merchants.Commands.UpdateMerchant;
using Merchant.Application.Merchants.Dto;
using Merchant.Application.Merchants.Dto.Requests;
using Merchant.Application.Shared.Dto;
using Merchant.Application.Stores;
using Merchant.Application.Stores.Commands.CreateStore;
using Merchant.Application.Stores.Commands.DeleteStore;
using Merchant.Application.Stores.Commands.UpdateLocation;
using Merchant.Application.Stores.Commands.UpdateStore;
using Merchant.Application.Stores.Dto;
using Merchant.Application.Stores.Dto.Request;
using Microsoft.AspNetCore.Authorization;

namespace Merchant.Api.GrpcServices
{
    [Authorize]
    public class MerchantGrpcService : MerchantApiGrpcService.MerchantApiGrpcServiceBase
    {
        private readonly StoreManager _storeManager;
        private readonly MerchantManager _merchantManager;

        public MerchantGrpcService(StoreManager storeManager,
            MerchantManager merchantManager)
        {
            _storeManager = storeManager;
            _merchantManager = merchantManager;
        }

        public override async Task<MerchantMessage> CreateMerchant(CreateMerchantMessage request,
            ServerCallContext context)
        {
            var response = await _merchantManager.CreateAsync(new CreateMerchantCommand
            {
                Description = request.Description,
                Email = request.Email,
                Name = request.Name,
                Slogan = request.Slogan,
                PhoneNumber = request.PhoneNumber,
                TaxNumber = request.TaxNumber,
                WebSiteUrl = request.WebsiteUrl
            });

            return As(response);
        }

        public override async Task<MerchantMessage> UpdateMerchant(UpdateMerchantMessage request,
            ServerCallContext context)
        {
            var updateMerchantModel = new UpdateMerchantDto
            {
                Description = request.Merchant.Description,
                Email = request.Merchant.Email,
                Name = request.Merchant.Name,
                Slogan = request.Merchant.Slogan,
                PhoneNumber = request.Merchant.PhoneNumber,
                TaxNumber = request.Merchant.TaxNumber,
                WebSiteUrl = request.Merchant.WebsiteUrl
            };

            var response = await _merchantManager.UpdateAsync(new UpdateMerchantCommand(request.MerchantId.ToGuid(),
                updateMerchantModel));

            return As(response);
        }

        public override async Task<DeleteMerchantResponseMessage> DeleteMerchant(ByIdMessage request,
            ServerCallContext context)
        {
            var isSucceed = await _merchantManager.DeleteAsync(new DeleteMerchantCommand(request.Id.ToGuid()));
            return new DeleteMerchantResponseMessage {IsSucceed = isSucceed};
        }

        public override async Task<MerchantMessage> GetMerchantById(ByIdMessage request,
            ServerCallContext context)
        {
            var response = await _merchantManager.GetByIdAsync(request.Id.ToGuid());
            return As(response);
        }

        public override async Task<StoreMessage> CreateStore(CreateStoreMessage request,
            ServerCallContext context)
        {
            var response = await _storeManager.CreateAsync(new CreateStoreCommand
            {
                Name = request.Name,
                Description = request.Description,
                Email = request.Email,
                Slogan = request.Slogan,
                MerchantId = request.MerchantId.ToGuid(),
                PhoneNumber = request.PhoneNumber,
                Location = new StoreLocationDto
                {
                    Address = request.Location.Address,
                    Country = new IdNameDto
                    {
                        RefId = request.Location.Country.Id.ToGuid(),
                        Name = request.Location.Country.Name
                    },
                    City = new IdNameDto
                    {
                        RefId = request.Location.City.Id.ToGuid(),
                        Name = request.Location.City.Name
                    },
                    District = new IdNameDto
                    {
                        RefId = request.Location.District.Id.ToGuid(),
                        Name = request.Location.District.Name
                    },
                    Neighborhood = new IdNameDto
                    {
                        RefId = request.Location.Neighborhood.Id.ToGuid(),
                        Name = request.Location.Neighborhood.Name
                    },
                    Quarter = new IdNameDto
                    {
                        RefId = request.Location.Quarter.Id.ToGuid(),
                        Name = request.Location.Quarter.Name
                    }
                }
            });

            return As(response);
        }

        public override async Task<StoreMessage> UpdateStore(UpdateStoreMessage request,
            ServerCallContext context)
        {
            var updateStoreModel = new UpdateStoreDto
            {
                Name = request.Store.Name,
                Description = request.Store.Description,
                Email = request.Store.Email,
                Slogan = request.Store.Slogan,
                PhoneNumber = request.Store.PhoneNumber,
            };

            var response =
                await _storeManager.UpdateAsync(new UpdateStoreCommand(request.StoreId.ToGuid(), updateStoreModel));

            return As(response);
        }

        public override async Task<StoreLocationMessage> UpdateLocation(UpdateStoreLocationMessage request, ServerCallContext context)
        {
            var storeLocation = new StoreLocationDto
            {
                Country = new IdNameDto
                {
                    RefId = request.Location.Country.Id.ToGuid(),
                    Name = request.Location.Country.Name
                },
                City = new IdNameDto
                {
                    RefId = request.Location.City.Id.ToGuid(),
                    Name = request.Location.City.Name
                },
                District = new IdNameDto
                {
                    RefId = request.Location.District.Id.ToGuid(),
                    Name = request.Location.District.Name
                },
                Neighborhood = new IdNameDto
                {
                    RefId = request.Location.Neighborhood.Id.ToGuid(),
                    Name = request.Location.Neighborhood.Name
                },
                Quarter = new IdNameDto
                {
                    RefId = request.Location.Quarter.Id.ToGuid(),
                    Name = request.Location.Quarter.Name
                },
                Address = request.Location.Address
            };

            var response = await _storeManager.UpdateLocationAsync(new UpdateLocationCommand(request.StoreId.ToGuid(), storeLocation));

            return new StoreLocationMessage
            {
                Country = new IdName
                {
                    Id = request.Location.Country.Id,
                    Name = request.Location.Country.Name
                },
                City = new IdName
                {
                    Id = request.Location.City.Id,
                    Name = request.Location.City.Name
                },
                District = new IdName
                {
                    Id = request.Location.District.Id,
                    Name = request.Location.District.Name
                },
                Neighborhood = new IdName
                {
                    Id = request.Location.Neighborhood.Id,
                    Name = request.Location.Neighborhood.Name
                },
                Quarter = new IdName
                {
                    Id = request.Location.Quarter.Id,
                    Name = request.Location.Quarter.Name
                },
                Address = request.Location.Address
            };
        }

        public override async Task<DeleteStoreResponseMessage> DeleteStore(ByIdMessage request,
            ServerCallContext context)
        {
            var isSucceed = await _storeManager.DeleteAsync(new DeleteStoreCommand(request.Id.ToGuid()));
            return new DeleteStoreResponseMessage {IsSucceed = isSucceed};
        }

        public override async Task<StoreMessage> GetStoreById(ByIdMessage request,
            ServerCallContext context)
        {
            var store = await _storeManager.GetByIdAsync(request.Id.ToGuid());
            return As(store);
        }

        #region Model Converters

        private StoreMessage As(StoreDto store)
        {
            return new()
            {
                Id = store.Id.ToString(),
                Description = store.Description,
                Email = store.Email,
                Merchant = new IdName
                {
                    Id = store.Merchant.RefId.ToString(),
                    Name = store.Merchant.Name
                },
                Name = store.Name,
                Slogan = store.Slogan,
                CreatedDate = store.CreatedDate.ToTimestamp(),
                EmailConfirmed = store.EmailConfirmed,
                ModifiedDate = store.ModifiedDate?.ToTimestamp(),
                PhoneNumber = store.PhoneNumber,
                Location = new StoreLocationMessage
                {
                    Address = store.Location.Address,
                    Country = new IdName
                    {
                        Id = store.Location.Country.RefId.ToString(),
                        Name = store.Location.Country.Name,
                    },
                    City = new IdName
                    {
                        Id = store.Location.City.RefId.ToString(),
                        Name = store.Location.City.Name,
                    },
                    District = new IdName
                    {
                        Id = store.Location.District.RefId.ToString(),
                        Name = store.Location.District.Name,
                    },
                    Neighborhood = new IdName
                    {
                        Id = store.Location.Neighborhood.RefId.ToString(),
                        Name = store.Location.Neighborhood.Name,
                    },
                    Quarter = new IdName
                    {
                        Id = store.Location.Quarter.RefId.ToString(),
                        Name = store.Location.Quarter.Name,
                    }
                }
            };
        }

        private MerchantMessage As(MerchantDto merchant)
        {
            return new MerchantMessage
            {
                Id = merchant.Id.ToString(),
                Description = merchant.Description,
                Email = merchant.Email,
                Name = merchant.Name,
                Slogan = merchant.Slogan,
                CreatedDate = merchant.CreatedDate.ToTimestamp(),
                EmailConfirmed = merchant.EmailConfirmed,
                ModifiedDate = merchant.ModifiedDate?.ToTimestamp(),
                PhoneNumber = merchant.PhoneNumber,
                TaxNumber = merchant.TaxNumber,
                WebsiteUrl = merchant.WebSiteUrl
            };
        }

        #endregion
    }
}