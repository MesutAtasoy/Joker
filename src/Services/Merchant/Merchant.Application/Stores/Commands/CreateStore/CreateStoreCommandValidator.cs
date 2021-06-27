using FluentValidation;

namespace Merchant.Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
    {
        public CreateStoreCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Slogan).MaximumLength(250).NotEmpty();
        }
    }
}