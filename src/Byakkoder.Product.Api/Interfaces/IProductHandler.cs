using Byakkoder.Product.Api.Dto;

namespace Byakkoder.Product.Api.Interfaces
{
    public interface IProductHandler
    {
        Task<ProductDto> GetById(long id);
        Task<BasicProductDto> Insert(CreateProductDto createProductDto);
        Task Update(UpdateProductDto updateProductDto);
    }
}