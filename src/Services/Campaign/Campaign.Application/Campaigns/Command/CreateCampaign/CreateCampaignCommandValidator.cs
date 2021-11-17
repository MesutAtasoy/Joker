using FluentValidation;

namespace Campaign.Application.Campaigns.Command.CreateCampaign;

public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
{
    public CreateCampaignCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Store.RefId).NotEmpty();
        RuleFor(x => x.Store.Name).NotEmpty();
        RuleFor(x => x.Merchant.RefId).NotEmpty();
        RuleFor(x => x.Merchant.Name).NotEmpty();

    }
}