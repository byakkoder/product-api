using Byakkoder.Product.Application.Exceptions;
using Byakkoder.Product.Application.Interfaces;
using Byakkoder.Product.Application.Models;
using MediatR;

namespace Byakkoder.Product.Application.Products.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, Domain.Entities.Product>
    {
        #region Fields
        
        private readonly IProductRepository _productRepository;
        private readonly IDiscountApiService _discountApiService;

        #endregion

        #region Constructor

        public GetByIdQueryHandler(
            IProductRepository productRepository,
            IDiscountApiService discountApiService)
        {
            _productRepository = productRepository;
            _discountApiService = discountApiService;
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

            DiscountDto discountDto = await _discountApiService.GetDiscountByProductId(product.ProductId);
            product.Discount = discountDto != null ? discountDto.Discount : 0;

            return product;
        } 

        #endregion
    }
}
