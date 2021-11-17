using FluentValidation;

namespace Merchant.Application.Stores.Commands.CreateStore;

public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
{
    public CreateStoreCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Slogan).MaximumLength(250).NotEmpty();
            
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