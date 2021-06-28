using FluentValidation;

namespace Campaign.Application.Campaigns.Command.CreateCampaign
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
    {
        public CreateCampaignCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}