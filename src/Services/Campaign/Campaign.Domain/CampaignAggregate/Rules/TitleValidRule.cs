using Joker.Domain.BusinessRule;

namespace Campaign.Domain.CampaignAggregate.Rules;

public class TitleValidRule : IBusinessRule
{
    private readonly string _title;

    public TitleValidRule(string title)
    {
        _title = title;
    }

    public bool IsBroken()
    {
        return string.IsNullOrWhiteSpace(_title);
    }

    public string Message => "Title is required";
}