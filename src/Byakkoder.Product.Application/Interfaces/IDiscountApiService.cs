using Byakkoder.Product.Application.Models;

namespace Byakkoder.Product.Application.Interfaces
{
    public interface IDiscountApiService
    {
        Task<DiscountDto> GetDiscountByProductId(string productId);
    }
}