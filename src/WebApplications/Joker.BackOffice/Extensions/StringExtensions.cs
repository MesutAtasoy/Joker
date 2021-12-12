namespace Joker.BackOffice.Extensions;

public static class StringExtensions
{
    public static string GetTime(this DateTime dateTime)
    {
        var remainResult = DateTime.UtcNow.Subtract(dateTime);

        if (remainResult.Days > 0)
        {
            return $"{remainResult.Days} day(s) ago";
        }

        if (remainResult.Hours > 0)
        {
            return $"{remainResult.Hours} hour(s) ago";
        }

        if (remainResult.Minutes > 0)
        {
            return $"{remainResult.Minutes} minute(s) ago";
        }

        return "now";
    }
}