using AutoMapper;
using Management.Api.Grpc;
using Management.Core.Entities;

namespace Management.Api.GrpcServices
{
    public class ManagementGrpcMappingProfile : Profile
    {
        public ManagementGrpcMappingProfile()
        {
            CreateMap<Currency, IdNameMessage>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Name, opt =>
                opt.MapFrom(x => x.Name));
            
            CreateMap<BusinessDirectory, IdNameMessage>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name));
            
            CreateMap<Language, IdNameMessage>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name));
            
            CreateMap<PricingPlan, IdNameMessage>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name));
            
            CreateMap<PaymentMethod, IdNameMessage>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name));
        }
    }
}