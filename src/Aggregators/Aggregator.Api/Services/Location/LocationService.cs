using System.Threading.Tasks;
using Aggregator.Api.Models.Location;
using Aggregator.Api.Models.Shared;
using Joker.Extensions;
using Location.Api.Grpc;

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
            
            locationModel.Country = new IdNameModel
            {
                Id = response.Country.Id.ToGuid(),
                Name = response.Country.Name
            };
            
            locationModel.City = new IdNameModel
            {
                Id = response.City.Id.ToGuid(),
                Name = response.City.Name
            };
            
            locationModel.District = new IdNameModel
            {
                Id = response.District.Id.ToGuid(),
                Name = response.District.Name
            };
            
            locationModel.Neighborhood = new IdNameModel
            {
                Id = response.Neighborhood.Id.ToGuid(),
                Name = response.Neighborhood.Name
            };
            
            locationModel.Quarter = new IdNameModel
            {
                Id = response.Quarter.Id.ToGuid(),
                Name = response.Quarter.Name
            };

            return locationModel;
        }
    }
}