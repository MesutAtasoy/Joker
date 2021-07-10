using System.Threading;
using System.Threading.Tasks;
using Location.Application.Quarters.Dto;
using MediatR;

namespace Location.Application.Quarters.Commands.Verification
{
    public class LocationVerificationCommandHandler : IRequestHandler<LocationVerificationCommand, LocationDto>
    {
        private readonly LocationManager _locationManager;
        
        public LocationVerificationCommandHandler(LocationManager locationManager)
        {
            _locationManager = locationManager;
        }

        public async Task<LocationDto> Handle(LocationVerificationCommand request, CancellationToken cancellationToken)
        {
            return await _locationManager.ValidateAsync(request);
        }
    }
}