using AutoMapper;
using Byakkoder.Product.Api.Dto;
using Byakkoder.Product.Application.Products.Commands.Insert;
using Byakkoder.Product.Application.Products.Commands.Update;

namespace Byakkoder.Product.Api.Mappings
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile() 
        {
            CreateMap<CreateProductDto, InsertCommand>();
            CreateMap<Application.Models.ProductDto, BasicProductDto>();
            CreateMap<UpdateProductDto, UpdateCommand>();
            CreateMap<Application.Models.ProductDto, ProductDto>().ReverseMap();
        }
    }
}
