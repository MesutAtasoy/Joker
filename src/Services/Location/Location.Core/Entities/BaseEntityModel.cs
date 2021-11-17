namespace Location.Core.Entities
{
    public abstract class BaseEntityModel
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}