using AutoMapper;
using Management.Api.Grpc;
using Management.Core.Entities;

namespace Management.Api.GrpcServices
{
    public class ManagementGrpcMappingProfile : Profile
    {
        public ManagementGrpcMappingProfile()
        {
            CreateMap<Currency, IdName>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Name, opt =>
                opt.MapFrom(x => x.Name));
            
            CreateMap<BusinessDirectory, IdName>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name));
            
            CreateMap<Language, IdName>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name));
            
            CreateMap<PricingPlan, IdName>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name));
            
            CreateMap<PaymentMethod, IdName>()
                .ForMember(x => x.Id, opt =>
                    opt.MapFrom(x => x.Id.ToString()))
                .ForMember(x => x.Name, opt =>
                    opt.MapFrom(x => x.Name));
        }
    }
}