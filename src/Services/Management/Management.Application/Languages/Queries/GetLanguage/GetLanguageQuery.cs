using System.Collections.Generic;
using Management.Core.Entities;
using MediatR;

namespace Management.Application.Languages.Queries.GetLanguage
{
    public class GetLanguageQuery : IRequest<List<Language>>
    {
    }
}