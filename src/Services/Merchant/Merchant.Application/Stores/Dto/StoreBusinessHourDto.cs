namespace Merchant.Application.Stores.Dto
{
    public class StoreBusinessHourDto
    {
        public int DayOfWeek { get; set; }
        public string DayName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool? IsTwentyFourHour { get; set; }
    }
}