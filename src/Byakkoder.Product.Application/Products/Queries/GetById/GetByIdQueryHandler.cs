using Byakkoder.Product.Application.Exceptions;
using Byakkoder.Product.Application.Interfaces;
using MediatR;

namespace Byakkoder.Product.Application.Products.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Domain.Entities.Product>
    {
        #region Fields
        
        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructor
        
        public GetByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion

        #region Handler Implementation
        
        public async Task<Domain.Entities.Product> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productRepository.GetById(request.Id);
            if (product == null)
            {
                throw new NotFoundException($"A product with the id {request.Id} was not found.");
            }

            return product;
        } 

        #endregion
    }
}
