using System.Threading.Tasks;
using Aggregator.Api.Models.Location;
using Location.Api.Grpc;
using IdName = Aggregator.Api.Models.Shared.IdName;

namespace Aggregator.Api.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly LocationApiGrpcService.LocationApiGrpcServiceClient _locationApiGrpcServiceClient;

        public LocationService(LocationApiGrpcService.LocationApiGrpcServiceClient locationApiGrpcServiceClient)
        {
            _locationApiGrpcServiceClient = locationApiGrpcServiceClient;
        }

        public async Task<LocationModel> ValidateAsync(LocationVerificationModel request)
        {
            var response = await _locationApiGrpcServiceClient.ValidateLocationAsync(new ValidateLocationMessage
            {
                CountryId = request.CountryId.ToString(),
                CityId = request.CityId.ToString(),
                DistrictId = request.DistrictId.ToString(),
                NeighborhoodId = request.NeighborhoodId.ToString(),
                QuarterId = request.QuarterId.ToString()
            });

            var locationModel = new LocationModel();
            
            if (!response.IsValid)
            {
                locationModel.IsValid = false;
                return locationModel;
            }

            locationModel.IsValid = true;
            
            locationModel.Country = new IdName
            {
                Id = response.Country.Id,
                Name = response.Country.Name
            };
            
            locationModel.City = new IdName
            {
                Id = response.City.Id,
                Name = response.City.Name
            };
            
            locationModel.District = new IdName
            {
                Id = response.District.Id,
                Name = response.District.Name
            };
            
            locationModel.Neighborhood = new IdName
            {
                Id = response.Neighborhood.Id,
                Name = response.Neighborhood.Name
            };
            
            locationModel.Quarter = new IdName
            {
                Id = response.Quarter.Id,
                Name = response.Quarter.Name
            };

            return locationModel;
        }
    }
}