using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Management.Core.Entities;
using Management.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Management.Application.Languages.Queries.GetLanguage
{
    public class GetLanguageQueryHandler : IRequestHandler<GetLanguageQuery, List<Language>>
    {
        private readonly ILanguageRepository _languageRepository;
        
        public GetLanguageQueryHandler(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository ;
        }

        public async Task<List<Language>> Handle(GetLanguageQuery request, CancellationToken cancellationToken)
        {
            var languages = await _languageRepository.Get(x=>!x.IsDeleted)
                .ToListAsync(cancellationToken);

            return languages;
        }
    }
}