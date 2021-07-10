using System.Threading.Tasks;
using Aggregator.Api.Models.Location;

namespace Aggregator.Api.Services.Location
{
    public interface ILocationService
    {
        Task<LocationModel> ValidateAsync(LocationVerificationModel request);
    }
}