using FluentValidation;

namespace Merchant.Application.Merchants.Commands.DeleteMerchant;

public class DeleteMerchantCommandValidator : AbstractValidator<DeleteMerchantCommand>
{
    public DeleteMerchantCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}