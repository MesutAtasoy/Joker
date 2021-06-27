using FluentValidation;

namespace Merchant.Application.Stores.Commands.AddBusinessHour
{
    public class AddBusinessHourCommandValidator : AbstractValidator<AddBusinessHourCommand>
    {
        public AddBusinessHourCommandValidator()
        {
            RuleFor(x => x.StoreId).NotEmpty();
            RuleFor(x => x.BusinessHour.DayOfWeek)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(6)
                .NotEmpty();
        }
    }
}