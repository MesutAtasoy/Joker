using FluentValidation;

namespace Merchant.Application.Stores.Commands.UpdateStore;

public class UpdateStoreCommandValidator : AbstractValidator<UpdateStoreCommand>
{
    public UpdateStoreCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Store.Name).NotEmpty();
        RuleFor(x => x.Store.Slogan).MaximumLength(250).NotEmpty();
    }
}