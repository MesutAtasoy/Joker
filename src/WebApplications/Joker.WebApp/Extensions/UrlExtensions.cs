using System.Web;

namespace Joker.WebApp.Extensions;

public static class UrlExtensions
{
    public static string GetUrlQueryString(object obj)
    {
        var properties = from p in obj.GetType().GetProperties()
            where p.GetValue(obj, null) != null
            select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null)?.ToString());

        var queryString = string.Join("&", properties.ToArray());

        return queryString;
    }
}