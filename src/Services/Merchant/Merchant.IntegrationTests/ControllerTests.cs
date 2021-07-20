using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bogus;
using Merchant.Application.Merchants.Commands.CreateMerchant;
using Merchant.Application.Merchants.Dto;
using Merchant.Application.Merchants.Dto.Requests;
using Merchant.Application.Shared.Dto;
using Merchant.Application.Stores.Commands.CreateStore;
using Merchant.Application.Stores.Dto;
using Merchant.Application.Stores.Dto.Request;
using Merchant.IntegrationTests.Fixtures;
using Xunit;

namespace Merchant.IntegrationTests
{
    public class ControllerTests : IClassFixture<AppTestFixture>
    {
        private readonly AppTestFixture _appTestFixture;
        private readonly HttpClient _client;

        public ControllerTests(AppTestFixture appTestFixture)
        {
            _appTestFixture = appTestFixture;
            _client = appTestFixture.CreateClient();
        }

        [Theory]
        [InlineData("api/healthcheck/api-status")]
        public async Task Given_Endpoints_Should_Return_Success_Http_Status_Code(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Create_Merchant_WhenValidData_Should_Return_Success_Http_Status_Code()
        {
            var faker = new Faker("en");

            var createMerchantCommand = new CreateMerchantCommand
            {
                Name = faker.Company.CompanyName(),
                Description = faker.Lorem.Sentences(2),
                Email = faker.Person.Email,
                PhoneNumber = faker.Person.Phone,
                Slogan = faker.Lorem.Sentences(1),
                TaxNumber = faker.Finance.Account(),
                WebSiteUrl = faker.Person.Website
            };

            var response = await _client.PostAsJsonAsync("api/Merchants", createMerchantCommand);
            var merchant = await response.Content.ReadFromJsonAsync<MerchantDto>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(createMerchantCommand.Name, merchant?.Name);
            Assert.Equal(createMerchantCommand.Email, merchant?.Email);
            Assert.Equal(createMerchantCommand.Description, merchant?.Description);
            Assert.Equal(createMerchantCommand.PhoneNumber, merchant?.PhoneNumber);
            Assert.Equal(createMerchantCommand.TaxNumber, merchant?.TaxNumber);
            Assert.Equal(createMerchantCommand.WebSiteUrl, merchant?.WebSiteUrl);
            Assert.Equal(createMerchantCommand.Slogan, merchant?.Slogan);
            Assert.NotNull(merchant?.Id);
            Assert.NotNull(merchant?.Slug);
            Assert.NotNull(merchant?.SlugKey);
        }

        [Fact]
        public async Task Update_Merchant_WhenValidData_Should_Return_Success_Http_Status_Code()
        {
            var merchant = await GetCreatedMerchant();

            var faker = new Faker("en");
            var updateMerchantDto = new UpdateMerchantDto
            {
                Name = faker.Company.CompanyName(),
                Description = faker.Lorem.Sentences(2),
                Email = faker.Person.Email,
                PhoneNumber = faker.Person.Phone,
                Slogan = faker.Lorem.Sentences(1),
                TaxNumber = faker.Finance.Account(),
                WebSiteUrl = faker.Person.Website
            };


            var response = await _client.PutAsJsonAsync($"api/Merchants/{merchant.Id}", updateMerchantDto);

            var updatedMerchant = await response.Content.ReadFromJsonAsync<MerchantDto>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(updateMerchantDto.Name, updatedMerchant?.Name);
            Assert.Equal(updateMerchantDto.Email, updatedMerchant?.Email);
            Assert.Equal(updateMerchantDto.Description, updatedMerchant?.Description);
            Assert.Equal(updateMerchantDto.PhoneNumber, updatedMerchant?.PhoneNumber);
            Assert.Equal(updateMerchantDto.TaxNumber, updatedMerchant?.TaxNumber);
            Assert.Equal(updateMerchantDto.WebSiteUrl, updatedMerchant?.WebSiteUrl);
            Assert.Equal(updateMerchantDto.Slogan, updatedMerchant?.Slogan);
            Assert.NotNull(updatedMerchant?.Id);
            Assert.NotNull(updatedMerchant?.Slug);
            Assert.NotNull(updatedMerchant?.SlugKey);
        }

        [Fact]
        public async Task Delete_Merchant_WhenValidData_Should_Return_Success_Http_Status_Code()
        {
            var merchant = await GetCreatedMerchant();

            var response = await _client.DeleteAsync($"api/Merchants/{merchant.Id}");

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Create_Store_WhenValidData_Should_Return_Success_Http_Status_Code()
        {
            var faker = new Faker("en");
            var merchant = await GetCreatedMerchant();

            var createStoreCommand = new CreateStoreCommand
            {
                Name = faker.Company.CompanyName(),
                Description = faker.Lorem.Sentences(2),
                Email = faker.Person.Email,
                PhoneNumber = faker.Person.Phone,
                Slogan = faker.Lorem.Sentences(1),
                Location = new StoreLocationDto
                {
                    Address = faker.Address.FullAddress(),
                    Country = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.Country()},
                    City = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.City()},
                    District = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.County()},
                    Neighborhood = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.State()},
                    Quarter = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.StreetName()}
                },
                MerchantId = merchant.Id
            };

            var response = await _client.PostAsJsonAsync("api/Stores", createStoreCommand);
            var store = await response.Content.ReadFromJsonAsync<StoreDto>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(createStoreCommand.Name, store?.Name);
            Assert.Equal(createStoreCommand.Email, store?.Email);
            Assert.Equal(createStoreCommand.Description, store?.Description);
            Assert.Equal(createStoreCommand.PhoneNumber, store?.PhoneNumber);
            Assert.Equal(createStoreCommand.MerchantId, store?.Merchant.RefId);
            ;
            Assert.Equal(createStoreCommand.Slogan, store?.Slogan);
            Assert.Equal(createStoreCommand.Location.Address, store?.Location.Address);
            Assert.NotNull(store?.Id);
        }

        [Fact]
        public async Task Update_Store_WhenValidData_Should_Return_Success_Http_Status_Code()
        {
            var store = await GetCreatedStore();

            var faker = new Faker("en");

            var updateStoreDto = new UpdateStoreDto
            {
                Name = faker.Company.CompanyName(),
                Description = faker.Lorem.Sentences(2),
                Email = faker.Person.Email,
                PhoneNumber = faker.Person.Phone,
                Slogan = faker.Lorem.Sentences(1)
            };


            var response = await _client.PutAsJsonAsync($"api/Stores/{store.Id}", updateStoreDto);

            var updatedStore = await response.Content.ReadFromJsonAsync<StoreDto>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(updateStoreDto.Name, updatedStore?.Name);
            Assert.Equal(updateStoreDto.Email, updatedStore?.Email);
            Assert.Equal(updateStoreDto.Description, updatedStore?.Description);
            Assert.Equal(updateStoreDto.PhoneNumber, updatedStore?.PhoneNumber);
            Assert.Equal(updateStoreDto.Slogan, updatedStore?.Slogan);
            Assert.NotNull(updatedStore?.Id);
        }

        [Fact]
        public async Task Update_Store_Location_WhenValidData_Should_Return_Success_Http_Status_Code()
        {
            var store = await GetCreatedStore();

            var faker = new Faker("en");

            var storeLocationDto = new StoreLocationDto
            {
                Address = faker.Address.FullAddress(),
                Country = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.Country()},
                City = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.City()},
                District = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.County()},
                Neighborhood = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.State()},
                Quarter = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.StreetName()}
            };


            var response = await _client.PutAsJsonAsync($"api/Stores/{store.Id}/Location", storeLocationDto);

            var updatedStoreLocation = await response.Content.ReadFromJsonAsync<StoreLocationDto>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(storeLocationDto.Country.RefId, updatedStoreLocation?.Country.RefId);
            Assert.Equal(storeLocationDto.City.RefId, updatedStoreLocation?.City.RefId);
            Assert.Equal(storeLocationDto.District.RefId, updatedStoreLocation?.District.RefId);
            Assert.Equal(storeLocationDto.Neighborhood.RefId, updatedStoreLocation?.Neighborhood.RefId);
            Assert.Equal(storeLocationDto.Quarter.RefId, updatedStoreLocation?.Quarter.RefId);
            Assert.Equal(storeLocationDto.Address, updatedStoreLocation?.Address);
        }

        [Fact]
        public async Task Delete_Store_WhenValidData_Should_Return_Success_Http_Status_Code()
        {
            var store = await GetCreatedStore();

            var response = await _client.DeleteAsync($"api/Stores/{store.Id}");

            Assert.True(response.IsSuccessStatusCode);
        }


        private async Task<StoreDto> GetCreatedStore()
        {
            var faker = new Faker("en");
            var merchant = await GetCreatedMerchant();

            var createStoreCommand = new CreateStoreCommand
            {
                Name = faker.Company.CompanyName(),
                Description = faker.Lorem.Sentences(2),
                Email = faker.Person.Email,
                PhoneNumber = faker.Person.Phone,
                Slogan = faker.Lorem.Sentences(1),
                Location = new StoreLocationDto
                {
                    Address = faker.Address.FullAddress(),
                    Country = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.Country()},
                    City = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.City()},
                    District = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.County()},
                    Neighborhood = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.State()},
                    Quarter = new IdNameDto {RefId = faker.Random.Guid(), Name = faker.Address.StreetName()}
                },
                MerchantId = merchant.Id
            };

            var response = await _client.PostAsJsonAsync("api/Stores", createStoreCommand);
            var store = await response.Content.ReadFromJsonAsync<StoreDto>();

            return store;
        }


        private async Task<MerchantDto> GetCreatedMerchant()
        {
            var faker = new Faker("en");

            var createMerchantCommand = new CreateMerchantCommand
            {
                Name = faker.Company.CompanyName(),
                Description = faker.Lorem.Sentences(2),
                Email = faker.Person.Email,
                PhoneNumber = faker.Person.Phone,
                Slogan = faker.Lorem.Sentences(1),
                TaxNumber = faker.Finance.Account(),
                WebSiteUrl = faker.Person.Website
            };

            var response = await _client.PostAsJsonAsync("api/Merchants", createMerchantCommand);
            var merchant = await response.Content.ReadFromJsonAsync<MerchantDto>();
            return merchant;
        }
    }
}