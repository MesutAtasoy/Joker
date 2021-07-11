using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using Joker.Exceptions;
using Joker.Extensions;
using Management.Api.Grpc;
using Management.Core.Repositories;

namespace Management.Api.GrpcServices
{
    public class ManagementGrpcService : ManagementApiGrpcService.ManagementApiGrpcServiceBase
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IBusinessDirectoryRepository _directoryRepository;
        private readonly IPricingPlanRepository _planRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IMapper _mapper;

        public ManagementGrpcService(ICurrencyRepository currencyRepository,
            ILanguageRepository languageRepository,
            IBusinessDirectoryRepository directoryRepository,
            IPricingPlanRepository planRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IMapper mapper)
        {
            _currencyRepository = currencyRepository;
            _languageRepository = languageRepository;
            _directoryRepository = directoryRepository;
            _planRepository = planRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _mapper = mapper;
        }

        public override async Task<IdName> GetCurrencyById(ByIdMessage request, ServerCallContext context)
        {
            var currency = await _currencyRepository.FirstOrDefaultAsync(x => x.Id == request.Id.ToGuid() && !x.IsDeleted);
            
            if (currency == null)
            {
                throw new NotFoundException("Currency is not found");
            }

            return _mapper.Map<IdName>(currency);
        }

        public override async Task<IdName> GetLanguageById(ByIdMessage request, ServerCallContext context)
        {
            var language = await _languageRepository.FirstOrDefaultAsync(x => x.Id == request.Id.ToGuid() && !x.IsDeleted);
            
            if (language == null)
            {
                throw new NotFoundException("Language is not found");
            }
            
            return _mapper.Map<IdName>(language);
        }

        public override async Task<IdName> GetBusinessDirectoryById(ByIdMessage request, ServerCallContext context)
        {
            var businessDirectory = await _directoryRepository.FirstOrDefaultAsync(x => x.Id == request.Id.ToGuid() && !x.IsDeleted);

            if (businessDirectory == null)
            {
                throw new NotFoundException("Business Directory is not found");
            }
            
            return _mapper.Map<IdName>(businessDirectory);
        }

        public override async Task<IdName> GetPricingPlanById(ByIdMessage request, ServerCallContext context)
        {
            var pricingPlan = await _planRepository.FirstOrDefaultAsync(x => x.Id == request.Id.ToGuid() && !x.IsDeleted);
            
            if (pricingPlan == null)
            {
                throw new NotFoundException("Pricing Plan is not found");
            }
            
            return _mapper.Map<IdName>(pricingPlan);
        }

        public override async Task<IdName> GetPaymentMethodById(ByIdMessage request, ServerCallContext context)
        {
            var paymentMethod = await _paymentMethodRepository.FirstOrDefaultAsync(x => x.Id == request.Id.ToGuid() && !x.IsDeleted);

            if (paymentMethod == null)
            {
                throw new NotFoundException("Payment Method is not found");
            }
            
            return _mapper.Map<IdName>(paymentMethod);
        }
    }
}