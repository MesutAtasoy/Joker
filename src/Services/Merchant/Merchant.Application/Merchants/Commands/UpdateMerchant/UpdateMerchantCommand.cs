using MediatR;
using Merchant.Application.Merchants.Dto;
using Merchant.Application.Merchants.Dto.Requests;

namespace Merchant.Application.Merchants.Commands.UpdateMerchant;

public class UpdateMerchantCommand : IRequest<MerchantDto>
{
    public UpdateMerchantCommand(Guid id, 
        UpdateMerchantDto merchant)
    {
        Id = id;
        Merchant = merchant;
    }

    public Guid Id { get; }
    public UpdateMerchantDto Merchant { get; }
}