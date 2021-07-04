using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Location.Application.Quarters.Dto;
using Location.Core.Repositories;
using MediatR;

namespace Location.Application.Quarters.Queries.GetQuarter
{
    public class GetQuarterQueryHandler : IRequestHandler<GetQuarterQuery, List<QuarterDto>>
    {
        private readonly IQuarterRepository _quarterRepository;
        private readonly IMapper _mapper;

        public GetQuarterQueryHandler(IQuarterRepository quarterRepository,
            IMapper mapper)
        {
            _quarterRepository = quarterRepository;
            _mapper = mapper;
        }

        public async Task<List<QuarterDto>> Handle(GetQuarterQuery request, CancellationToken cancellationToken)
        {
            var quarters =  await _quarterRepository.ByNeighborhoodIdAsync(request.NeighborhoodId);
            return _mapper.Map<List<QuarterDto>>(quarters);
        }
    }
}