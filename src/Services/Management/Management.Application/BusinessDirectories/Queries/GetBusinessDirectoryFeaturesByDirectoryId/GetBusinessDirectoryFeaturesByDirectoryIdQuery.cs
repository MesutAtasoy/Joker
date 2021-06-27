using System;
using System.Collections.Generic;
using Management.Core.Entities;
using MediatR;

namespace Management.Application.BusinessDirectories.Queries.GetBusinessDirectoryFeaturesByDirectoryId
{
    public class GetBusinessDirectoryFeaturesByDirectoryIdQuery : IRequest<List<BusinessDirectoryFeature>>
    {
        public GetBusinessDirectoryFeaturesByDirectoryIdQuery(Guid directoryId)
        {
            DirectoryId = directoryId;
        }

        public Guid DirectoryId { get; } 
    }
}