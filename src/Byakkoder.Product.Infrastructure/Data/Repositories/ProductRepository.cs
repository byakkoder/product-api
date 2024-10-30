using AutoMapper;
using Byakkoder.Product.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Byakkoder.Product.Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region Fields
        
        private readonly ProductManagementContext _productManagementContext;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        
        public ProductRepository(
            ProductManagementContext productManagementContext,
            IMapper mapper)
        {
            _productManagementContext = productManagementContext;
            _mapper = mapper;
        }

        #endregion

        #region Public Methods
        
        public async Task Create(Domain.Entities.Product product)
        {
            Data.Entities.Product productToCreate = _mapper.Map<Data.Entities.Product>(product);

            _productManagementContext.Products.Add(productToCreate);

            await _productManagementContext.SaveChangesAsync();

            product.Id = productToCreate.Id;
        }

        public async Task<Domain.Entities.Product> GetById(long id)
        {
            Data.Entities.Product product = await _productManagementContext.Products.FindAsync(id);

            return _mapper.Map<Domain.Entities.Product>(product);
        }

        public async Task<Domain.Entities.Product> GetByName(string name)
        {
            Data.Entities.Product product = await _productManagementContext.Products.FirstOrDefaultAsync(p => p.Name == name);

            return _mapper.Map<Domain.Entities.Product>(product);
        }

        public async Task Update(Domain.Entities.Product product)
        {
            Data.Entities.Product updatedProduct = _mapper.Map<Data.Entities.Product>(product);
            Data.Entities.Product existingProduct = await _productManagementContext.Products.FindAsync(product.Id);
            _productManagementContext.Entry(existingProduct).CurrentValues.SetValues(updatedProduct);

            await _productManagementContext.SaveChangesAsync();
        } 

        #endregion
    }
}
