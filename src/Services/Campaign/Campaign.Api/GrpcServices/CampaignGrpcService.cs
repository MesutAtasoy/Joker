using System.Threading.Tasks;
using Campaign.Api.Grpc;
using Grpc.Core;

namespace Campaign.Api.GrpcServices
{
    public class CampaignGrpcService : CampaignApiGrpcService.CampaignApiGrpcServiceBase
    {
        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return await Task.FromResult(new HelloReply
            {
                Message = request.Name + "1"
            });
        }
    }
}