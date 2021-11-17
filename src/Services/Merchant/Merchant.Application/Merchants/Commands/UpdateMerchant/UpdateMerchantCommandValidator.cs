using FluentValidation;

namespace Merchant.Application.Merchants.Commands.UpdateMerchant;

public class UpdateMerchantCommandValidator : AbstractValidator<UpdateMerchantCommand>
{
    public UpdateMerchantCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Merchant.Name).NotEmpty();
        RuleFor(x => x.Merchant.Slogan).MaximumLength(250).NotEmpty();
    }
}