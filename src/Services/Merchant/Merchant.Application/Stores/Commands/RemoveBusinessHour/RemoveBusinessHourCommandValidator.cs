using FluentValidation;

namespace Merchant.Application.Stores.Commands.RemoveBusinessHour
{
    public class RemoveBusinessHourCommandValidator : AbstractValidator<RemoveBusinessHourCommand>
    {
        public RemoveBusinessHourCommandValidator()
        {
            RuleFor(x => x.StoreId).NotEmpty();
            RuleFor(x => x.DayOfWeek)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(6)
                .NotEmpty();
        }
    }
}