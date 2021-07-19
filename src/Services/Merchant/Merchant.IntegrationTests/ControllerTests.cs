using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bogus;
using Merchant.Application.Merchants.Commands.CreateMerchant;
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

            var response = await _client.PostAsJsonAsync("api/Merchants",  createMerchantCommand);
            
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}