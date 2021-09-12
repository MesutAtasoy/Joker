using FluentValidation;

namespace Location.Application.Quarters.Commands.Verification
{
    public class LocationVerificationCommandValidator  : AbstractValidator<LocationVerificationCommand>
    {
        public LocationVerificationCommandValidator()
        {
            RuleFor(x => x.CountryId).NotEmpty().NotNull();
            RuleFor(x => x.CityId).NotEmpty().NotNull();
            RuleFor(x => x.DistrictId).NotEmpty().NotNull();
            RuleFor(x => x.NeighborhoodId).NotEmpty().NotNull();
            RuleFor(x => x.QuarterId).NotEmpty().NotNull();
        }
    }
}