using FluentValidation;

namespace Campaign.Application.Campaigns.Command.DeleteCampaign
{
    public class DeleteCampaignCommandValidator : AbstractValidator<DeleteCampaignCommand>
    {
        public DeleteCampaignCommandValidator()
        {
            RuleFor(x => x.CampaignId).NotEmpty();
        }
    }
}