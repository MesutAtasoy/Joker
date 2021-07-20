using System.Net.Http;
using System.Threading.Tasks;
using Merchant.IntegrationTests.Fixtures;
using Xunit;

namespace Merchant.IntegrationTests
{
    public class HealthCheckControllerTests : IClassFixture<AppTestFixture>
    {
        private readonly AppTestFixture _appTestFixture;
        private readonly HttpClient _client;

        public HealthCheckControllerTests(AppTestFixture appTestFixture)
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
    }
}