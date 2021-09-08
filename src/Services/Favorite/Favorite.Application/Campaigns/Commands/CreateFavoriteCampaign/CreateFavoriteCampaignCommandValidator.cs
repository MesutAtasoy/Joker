using FluentValidation;

namespace Favorite.Application.Campaigns.Commands.CreateFavoriteCampaign
{
    public class CreateFavoriteCampaignCommandValidator : AbstractValidator<CreateFavoriteCampaignCommand>
    {
        public CreateFavoriteCampaignCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Title).NotEmpty().NotNull();
            RuleFor(x => x.Slug).NotEmpty().NotNull();
            RuleFor(x => x.SlugKey).NotEmpty().NotNull();
        }
    }
}