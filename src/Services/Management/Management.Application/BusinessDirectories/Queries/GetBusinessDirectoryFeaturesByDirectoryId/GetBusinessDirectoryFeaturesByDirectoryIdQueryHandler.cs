using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;

namespace Management.Application.BusinessDirectories.Queries.GetBusinessDirectoryFeaturesByDirectoryId
{
    public class GetBusinessCategoryFeaturesByCategoryIdQueryHandler : IRequestHandler<GetBusinessDirectoryFeaturesByDirectoryIdQuery, List<BusinessDirectoryFeature>>
    {
        private readonly IBusinessDirectoryFeatureRepository _businessCategoryFeatureRepository;

        public GetBusinessCategoryFeaturesByCategoryIdQueryHandler(IBusinessDirectoryFeatureRepository businessCategoryFeatureRepository)
        {
            _businessCategoryFeatureRepository = businessCategoryFeatureRepository;
        }

        public async Task<List<BusinessDirectoryFeature>> Handle(GetBusinessDirectoryFeaturesByDirectoryIdQuery request,
            CancellationToken cancellationToken)
        {
            var directoryFeatures =  await Task.FromResult(_businessCategoryFeatureRepository
                .Get(x => !x.IsDeleted && x.BusinessDirectoryId == request.DirectoryId)
                .OrderBy(x => x.Order)
                .ToList());

            return directoryFeatures;
        }
    }
}