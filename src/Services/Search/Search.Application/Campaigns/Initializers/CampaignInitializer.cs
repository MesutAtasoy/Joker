using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Search.Core.IndexManagers.Campaign;
namespace Search.Application.Campaigns.Initializers;

public class CampaignInitializer : ICampaignInitializer
{
    private readonly ICampaignIndexManager _indexManager;
    private readonly ILogger<CampaignInitializer> _logger;

    public CampaignInitializer(ICampaignIndexManager indexManager,
        ILogger<CampaignInitializer> logger)
    {
        _indexManager = indexManager;
        _logger = logger;
    }

    public async Task InitializeAsync()
    {
        await _indexManager.CreateIndexAsync();
        _logger.LogInformation("Campaign Index Created");
    }
}