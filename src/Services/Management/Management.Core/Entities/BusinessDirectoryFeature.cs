using System;

namespace Management.Core.Entities
{
    public partial class BusinessDirectoryFeature : BaseEntityModel
    {
        public BusinessDirectoryFeature()
        {
        }
        
        public Guid BusinessDirectoryId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int Order { get; set; }

        public virtual BusinessDirectory BusinessDirectory { get; set; }
    }
}