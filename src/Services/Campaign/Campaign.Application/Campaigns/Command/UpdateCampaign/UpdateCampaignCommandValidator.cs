using FluentValidation;

namespace Campaign.Application.Campaigns.Command.UpdateCampaign
{
    public class UpdateCampaignCommandValidator : AbstractValidator<UpdateCampaignCommand>
    {
        public UpdateCampaignCommandValidator()
        {
            RuleFor(x => x.CampaignId).NotEmpty();
            RuleFor(x => x.Campaign.Title).NotEmpty();
        }
    }
}