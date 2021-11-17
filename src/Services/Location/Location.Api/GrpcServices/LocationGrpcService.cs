using Grpc.Core;
using Joker.Extensions;
using Location.Api.Grpc;
using Location.Application;
using Location.Application.Quarters.Commands.Verification;

namespace Location.Api.GrpcServices;

public class LocationGrpcService : LocationApiGrpcService.LocationApiGrpcServiceBase
{
    private readonly LocationManager _locationManager;

    public LocationGrpcService(LocationManager locationManager)
    {
        _locationManager = locationManager;
    }

    public override async Task<ValidateLocationResponseMessage> ValidateLocation(ValidateLocationMessage request,
        ServerCallContext context)
    {
        var response = await _locationManager.ValidateAsync(new LocationVerificationCommand
        {
            CountryId = request.CountryId.ToGuid(),
            CityId = request.CityId.ToGuid(),
            DistrictId = request.DistrictId.ToGuid(),
            NeighborhoodId = request.NeighborhoodId.ToGuid(),
            QuarterId = request.QuarterId.ToGuid(),
        });
            
        var responseMessage = new ValidateLocationResponseMessage
        {
            IsValid = response.IsValid
        };

        if (!response.IsValid)
        {
            return responseMessage;
        }

        responseMessage.Country = new IdName
        {
            Id = response.Country.Id.ToString(),
            Name = response.Country.Name
        };
        responseMessage.City = new IdName
        {
            Id = response.City.Id.ToString(),
            Name = response.City.Name
        };
        responseMessage.District = new IdName
        {
            Id = response.District.Id.ToString(),
            Name = response.District.Name
        };
        responseMessage.Neighborhood = new IdName
        {
            Id = response.Neighborhood.Id.ToString(),
            Name = response.Neighborhood.Name
        };
        responseMessage.Quarter = new IdName
        {
            Id = response.Quarter.Id.ToString(),
            Name = response.Quarter.Name
        };

        return responseMessage;
    }
}