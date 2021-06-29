namespace Search.Core.IndexModels
{
    public sealed class BadgeIndexModel
    {
        public BadgeIndexModel()
        {
        }

        public BadgeIndexModel(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}