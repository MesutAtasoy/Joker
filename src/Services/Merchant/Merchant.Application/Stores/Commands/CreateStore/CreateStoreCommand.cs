using System;
using MediatR;

namespace Merchant.Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommand : IRequest<Guid>
    {
        public Guid MerchantId { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}