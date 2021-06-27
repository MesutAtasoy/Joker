using FluentValidation;

namespace Merchant.Application.Stores.Commands.RemoveFaq
{
    public class RemoveFaqCommandValidator : AbstractValidator<RemoveFaqCommand>
    {
        public RemoveFaqCommandValidator()
        {
            RuleFor(x => x.FaqId).NotEmpty();
            RuleFor(x => x.StoreId).NotEmpty();
        }
    }
}