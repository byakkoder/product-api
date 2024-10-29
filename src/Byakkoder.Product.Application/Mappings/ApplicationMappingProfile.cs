using AutoMapper;
using Byakkoder.Product.Application.Models;
using Byakkoder.Product.Application.Products.Commands.Insert;
using Byakkoder.Product.Application.Products.Commands.Update;

namespace Byakkoder.Product.Application.Mappings
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile() 
        {
            CreateMap<InsertCommand, Domain.Entities.Product>();
            CreateMap<UpdateCommand, Domain.Entities.Product>();
            CreateMap<Domain.Entities.Product, ProductDto>();
        }
    }
}
