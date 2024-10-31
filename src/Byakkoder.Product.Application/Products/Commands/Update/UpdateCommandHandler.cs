using AutoMapper;
using Byakkoder.Product.Application.Exceptions;
using Byakkoder.Product.Application.Interfaces;
using MediatR;

namespace Byakkoder.Product.Application.Products.Commands.Update
{
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Unit>
    {
        #region Fields

        private readonly IProductRepository _productRepository;
        private readonly IProductStatusService _productStatusService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public UpdateCommandHandler(
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

        public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productRepository.GetById(request.Id);
            if (product == null)
            {
                throw new NotFoundException($"A product with the id {request.Id} was not found.");
            }

            product = await _productRepository.GetByName(request.Name);
            if (product != null && product.Id != request.Id)
            {
                throw new ItemExistsException($"A product with the name {request.Name} currently exists.");
            }

            product = await _productRepository.GetByProductId(request.ProductId);
            if (product != null && product.Id != request.Id)
            {
                throw new ItemExistsException($"A product with the ProductId {request.ProductId} currently exists.");
            }

            product = _mapper.Map<Domain.Entities.Product>(request);

            Dictionary<int, string> productsDict = _productStatusService.GetProductStatusDict();
            if (productsDict != null)
            {
                Dictionary<string, int> productsReverseDict = productsDict.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
                product.Status = productsReverseDict.ContainsKey(request.StatusName) ? Convert.ToBoolean(productsReverseDict[request.StatusName]) : false;
            }

            await _productRepository.Update(product);

            return Unit.Value;
        }

        #endregion
    }
}
