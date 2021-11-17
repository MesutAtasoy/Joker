using FluentValidation;

namespace Merchant.Application.Merchants.Commands.CreateMerchant;

public class CreateMerchantCommandValidator :  AbstractValidator<CreateMerchantCommand>
{
    public CreateMerchantCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.PricingPlan.RefId).NotEmpty();
        RuleFor(x => x.PricingPlan.Name).NotEmpty();
        RuleFor(x => x.Slogan).MaximumLength(250).NotEmpty();
    }
}