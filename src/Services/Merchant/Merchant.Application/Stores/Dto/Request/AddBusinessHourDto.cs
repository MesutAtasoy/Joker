namespace Merchant.Application.Stores.Dto.Request;

public class AddBusinessHourDto
{
    public int DayOfWeek { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public bool? IsTwentyFourHour { get; set; }
}