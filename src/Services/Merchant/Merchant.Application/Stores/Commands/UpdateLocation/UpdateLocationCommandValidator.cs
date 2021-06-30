using FluentValidation;

namespace Merchant.Application.Stores.Commands.UpdateLocation
{
    public class UpdateLocationCommandValidator: AbstractValidator<UpdateLocationCommand>
    {
        public UpdateLocationCommandValidator()
        {
            RuleFor(x => x.StoreId).NotEmpty();
            
            RuleFor(x => x.Location.Country.RefId).NotEmpty();
            RuleFor(x => x.Location.Country.Name).NotEmpty();
            
            RuleFor(x => x.Location.City.RefId).NotEmpty();
            RuleFor(x => x.Location.City.Name).NotEmpty();
            
            RuleFor(x => x.Location.Neighborhood.RefId).NotEmpty();
            RuleFor(x => x.Location.Neighborhood.Name).NotEmpty();
            
            RuleFor(x => x.Location.Quarter.RefId).NotEmpty();
            RuleFor(x => x.Location.Quarter.Name).NotEmpty();
            
            RuleFor(x => x.Location.District.RefId).NotEmpty();
            RuleFor(x => x.Location.District.Name).NotEmpty();
            
            RuleFor(x => x.Location.Address).NotEmpty();
        }
    }
}