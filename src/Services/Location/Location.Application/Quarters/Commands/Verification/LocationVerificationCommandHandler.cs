using System.Threading;
using System.Threading.Tasks;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Quarters.Commands.Verification
{
    public class LocationVerificationCommandHandler : IRequestHandler<LocationVerificationCommand, bool>
    {
        private readonly IQuarterRepository _quarterRepository;

        public LocationVerificationCommandHandler(IQuarterRepository quarterRepository)
        {
            _quarterRepository = quarterRepository;
        }

        public async Task<bool> Handle(LocationVerificationCommand request, CancellationToken cancellationToken)
        {
            return await _quarterRepository.ValidateAsync(request.CountryId,
                request.CityId,
                request.DistrictId,
                request.NeighborhoodId,
                request.QuarterId);
        }
    }
}