using FluentValidation;

namespace Favorite.Application.Campaigns.Commands.CreateFavoriteCampaign
{
    public class CreateFavoriteCampaignCommandValidator : AbstractValidator<CreateFavoriteCampaignCommand>
    {
        public CreateFavoriteCampaignCommandValidator()
        {
            RuleFor(x => x.Campaign.Id).NotEmpty().NotNull();
            RuleFor(x => x.Campaign.Name).NotEmpty().NotNull();
        }
    }
}