using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Application.BusinessDirectories.Queries.GetBusinessDirectories
{
    public class GetBusinessDirectoriesQueryHandler : IRequestHandler<GetBusinessDirectoriesQuery, List<BusinessDirectory>>
    {
        private readonly IBusinessDirectoryRepository _businessCategoryRepository;

        public GetBusinessDirectoriesQueryHandler(IBusinessDirectoryRepository businessCategoryRepository)
        {
            _businessCategoryRepository = businessCategoryRepository;
        }

        public async Task<List<BusinessDirectory>> Handle(GetBusinessDirectoriesQuery request,
            CancellationToken cancellationToken)
        {
            var businessDirectories = await _businessCategoryRepository
                    .Get(x => !x.IsDeleted)
                    .OrderBy(x => x.Order)
                    .ToListAsync(cancellationToken);

            return businessDirectories;
        }
    }
}