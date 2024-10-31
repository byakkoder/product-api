
namespace Byakkoder.Product.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Domain.Entities.Product> GetById(long id);

        Task<Domain.Entities.Product> GetByName(string name);

        Task Create(Domain.Entities.Product product);

        Task Update(Domain.Entities.Product product);
        Task<Domain.Entities.Product> GetByProductId(string productId);
    }
}
