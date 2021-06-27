using System.Threading;
using System.Threading.Tasks;
using Joker.Exceptions;
using MediatR;
using Merchant.Domain.MerchantAggregate.Repositories;

namespace Merchant.Application.Merchants.Commands.DeleteMerchant
{
    public class DeleteMerchantCommandHandler : IRequestHandler<DeleteMerchantCommand, bool>
    {
        private readonly IMerchantRepository _merchantRepository;
        
        public DeleteMerchantCommandHandler(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }
        
        public async Task<bool> Handle(DeleteMerchantCommand request, CancellationToken cancellationToken)
        {
            var merchant = await _merchantRepository.GetByIdAsync(request.Id);

            if (merchant == null)
            {
                throw new NotFoundException("Merchant is not found");
            }
            
            merchant.MarkAsDeleted();

            await _merchantRepository.UpdateAsync(merchant.Id, merchant);
            
            return true;
        }
    }
}