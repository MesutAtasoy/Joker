namespace Management.Core.Entities
{
    public partial class PaymentMethod : BaseEntityModel
    {
        public PaymentMethod()
        {
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
