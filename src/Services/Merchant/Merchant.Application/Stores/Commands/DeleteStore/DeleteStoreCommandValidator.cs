using FluentValidation;

namespace Merchant.Application.Stores.Commands.DeleteStore;

public class DeleteStoreCommandValidator : AbstractValidator<DeleteStoreCommand>
{
    public DeleteStoreCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}