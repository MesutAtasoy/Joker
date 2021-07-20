using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Bogus;
using Merchant.Application.Merchants.Commands.CreateMerchant;
using Merchant.Application.Merchants.Dto;
using Merchant.Application.Merchants.Dto.Requests;
using Merchant.IntegrationTests.Fixtures;
using Xunit;

namespace Merchant.IntegrationTests
{
    public class MerchantControllerTests : IClassFixture<AppTestFixture>
    {
        private readonly AppTestFixture _appTestFixture;
        private readonly HttpClient _client;
        
        public MerchantControllerTests(AppTestFixture appTestFixture)
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

            var response = await _client.PostAsJsonAsync("api/Merchants",  createMerchantCommand);
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

            var response = await _client.PostAsJsonAsync("api/Merchants",  createMerchantCommand);
            var merchant = await response.Content.ReadFromJsonAsync<MerchantDto>();
            return merchant;
        }
    }
}