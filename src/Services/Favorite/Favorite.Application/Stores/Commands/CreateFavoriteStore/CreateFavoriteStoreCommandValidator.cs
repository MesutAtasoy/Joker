using FluentValidation;

namespace Favorite.Application.Stores.Commands.CreateFavoriteStore
{
    public class CreateFavoriteStoreCommandValidator : AbstractValidator<CreateFavoriteStoreCommand>
    {
        public CreateFavoriteStoreCommandValidator()
        {
            RuleFor(x => x.Store.Id).NotEmpty().NotNull();
            RuleFor(x => x.Store.Name).NotEmpty().NotNull();
        }
    }
}