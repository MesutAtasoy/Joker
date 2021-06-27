namespace Merchant.Application.Stores.Dto.Request
{
    public class AddFaqDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Order { get; set; }
    }
}