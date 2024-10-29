using Byakkoder.Product.Api.Dto;

namespace Byakkoder.Product.Api.Interfaces
{
    public interface IProductHandler
    {
        Task<ProductDto> GetById(long id);
        Task<ProductDto> Insert(ProductDto productDto);
        Task Update(UpdateProductDto updateProductDto);
    }
}