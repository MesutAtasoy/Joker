using FluentValidation;

namespace Favorite.Application.Stores.Commands.CreateFavoriteStore
{
    public class CreateFavoriteStoreCommandValidator : AbstractValidator<CreateFavoriteStoreCommand>
    {
        public CreateFavoriteStoreCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Slug).NotEmpty().NotNull();
            RuleFor(x => x.SlugKey).NotEmpty().NotNull();
        }
    }
}