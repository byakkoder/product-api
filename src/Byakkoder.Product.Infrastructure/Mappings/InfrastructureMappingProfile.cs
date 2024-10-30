using AutoMapper;

namespace Byakkoder.Product.Infrastructure.Mappings
{
    public class InfrastructureMappingProfile : Profile
    {
        public InfrastructureMappingProfile() 
        { 
            CreateMap<Domain.Entities.Product, Data.Entities.Product>().ReverseMap();
        }
    }
}
