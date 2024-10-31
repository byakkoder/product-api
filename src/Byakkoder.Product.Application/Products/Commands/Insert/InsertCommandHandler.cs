using AutoMapper;
using Byakkoder.Product.Application.Exceptions;
using Byakkoder.Product.Application.Interfaces;
using Byakkoder.Product.Application.Models;
using MediatR;

namespace Byakkoder.Product.Application.Products.Commands.Insert
{
    public class InsertCommandHandler : IRequestHandler<InsertCommand, Application.Models.ProductDto>
    {
        #region Fields
        
        private readonly IProductRepository _productRepository;
        private readonly IProductStatusService _productStatusService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public InsertCommandHandler(
            IProductRepository productRepository,
            IProductStatusService productStatusService,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _productStatusService = productStatusService;
            _mapper = mapper;
        }

        #endregion

        #region Handler Implementation

        public async Task<Application.Models.ProductDto> Handle(InsertCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productRepository.GetByName(request.Name);
            if (product != null)
            {
                throw new ItemExistsException("A product with the same name currently exists.");
            }

            product = await _productRepository.GetByProductId(request.ProductId);
            if (product != null)
            {
                throw new ItemExistsException("A product with the same ProductId currently exists.");
            }

            product = _mapper.Map<Domain.Entities.Product>(request);

            Dictionary<int, string> productsDict = _productStatusService.GetProductStatusDict();
            if (productsDict != null)
            {
                Dictionary<string, int> productsReverseDict = productsDict.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
                product.Status = productsReverseDict.ContainsKey(request.StatusName) ? Convert.ToBoolean(productsReverseDict[request.StatusName]) : false;
            }

            await _productRepository.Create(product);

            ProductDto productDto = _mapper.Map<ProductDto>(product);
            productDto.StatusName = productsDict != null && productsDict.ContainsKey(Convert.ToInt32(product.Status)) ? productsDict[Convert.ToInt32(product.Status)] : string.Empty;

            return productDto;
        } 

        #endregion
    }
}
