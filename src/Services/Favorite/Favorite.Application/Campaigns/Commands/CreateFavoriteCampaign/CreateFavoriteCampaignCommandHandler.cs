using System.Threading;
using System.Threading.Tasks;
using Favorite.Application.Campaigns.Dto;
using MediatR;

namespace Favorite.Application.Campaigns.Commands.CreateFavoriteCampaign
{
    public class CreateFavoriteCampaignCommandHandler : IRequestHandler<CreateFavoriteCampaignCommand, FavoriteCampaignDto>
    {
        private readonly FavoriteCampaignManager _manager;
        
        public CreateFavoriteCampaignCommandHandler(FavoriteCampaignManager manager)
        {
            _manager = manager;
        }
        
        public async Task<FavoriteCampaignDto> Handle(CreateFavoriteCampaignCommand request, CancellationToken cancellationToken)
        {
            return await _manager.AddFavoriteCampaignAsync(request);
        }
    }
}