using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Location.Core.Entities;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Quarters.Queries.GetQuarter
{
    public class GetQuarterQueryHandler : IRequestHandler<GetQuarterQuery, List<Quarter>>
    {
        private readonly IQuarterRepository _quarterRepository;

        public GetQuarterQueryHandler(IQuarterRepository quarterRepository)
        {
            _quarterRepository = quarterRepository;
        }

        public async Task<List<Quarter>> Handle(GetQuarterQuery request, CancellationToken cancellationToken)
        {
            return await _quarterRepository.ByNeighborhoodIdAsync(request.NeighborhoodId);
        }
    }
}