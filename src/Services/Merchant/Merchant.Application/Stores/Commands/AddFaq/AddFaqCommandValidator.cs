using FluentValidation;

namespace Merchant.Application.Stores.Commands.AddFaq
{
    public class AddFaqCommandValidator : AbstractValidator<AddFaqCommand>
    {
        public AddFaqCommandValidator()
        {
            RuleFor(x => x.StoreId).NotEmpty();
            RuleFor(x => x.Faq.Question).NotEmpty();
            RuleFor(x => x.Faq.Answer).NotEmpty();
        }
    }
}