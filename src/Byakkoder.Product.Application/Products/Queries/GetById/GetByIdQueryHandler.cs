using AutoMapper;
using Byakkoder.Product.Application.Exceptions;
using Byakkoder.Product.Application.Interfaces;
using Byakkoder.Product.Application.Models;
using MediatR;

namespace Byakkoder.Product.Application.Products.Queries.GetById
{
    public class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, ProductDto>
    {
        #region Fields
        
        private readonly IProductRepository _productRepository;
        private readonly IDiscountApiService _discountApiService;
        private readonly IProductStatusService _productStatusService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public GetByIdQueryHandler(
            IProductRepository productRepository,
            IDiscountApiService discountApiService,
            IProductStatusService productStatusService,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _discountApiService = discountApiService;
            _productStatusService = productStatusService;
            _mapper = mapper;
        }

        #endregion

        #region Handler Implementation
        
        public async Task<ProductDto> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productRepository.GetById(request.Id);
            if (product == null)
            {
                throw new NotFoundException($"A product with the id {request.Id} was not found.");
            }

            DiscountDto discountDto = await _discountApiService.GetDiscountByProductId(product.ProductId);
            product.Discount = discountDto != null ? discountDto.Discount : 0;
            
            ProductDto productDto = _mapper.Map<ProductDto>(product);

            Dictionary<int, string> productsDict = _productStatusService.GetProductStatusDict();
            int productStatusKey = Convert.ToInt32(product.Status);
            if (productsDict != null && productsDict.ContainsKey(productStatusKey))
            {
                productDto.StatusName = productsDict[productStatusKey];
            }

            return productDto;
        } 

        #endregion
    }
}
