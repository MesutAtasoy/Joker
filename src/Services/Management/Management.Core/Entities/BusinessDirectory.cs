namespace Management.Core.Entities;

public partial class BusinessDirectory : BaseEntityModel
{
    public BusinessDirectory()
    {
    }

    public string Name { get; set; }
    public string DisplayName { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public string ImageUrl { get; set; }
    public int Order { get; set; }
}