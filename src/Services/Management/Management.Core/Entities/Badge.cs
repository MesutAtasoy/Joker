namespace Management.Core.Entities
{
    public partial class Badge : BaseEntityModel
    {
        public Badge()
        {
        }
        
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string FontColor { get; set; }
        public string BackgroundColor { get; set; }
    }
}