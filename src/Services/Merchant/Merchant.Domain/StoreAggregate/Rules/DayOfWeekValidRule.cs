using Joker.Domain.BusinessRule;

namespace Merchant.Domain.StoreAggregate.Rules
{
    public sealed class DayOfWeekValidRule : IBusinessRule
    {
        private readonly int _dayOfWeek;

        public DayOfWeekValidRule(int dayOfWeek)
        {
            this._dayOfWeek = dayOfWeek;
        }

        public bool IsBroken()
        {
            return _dayOfWeek is < 0 or > 6;
        }

        public string Message => "Day of Week is invalid";
    }
}