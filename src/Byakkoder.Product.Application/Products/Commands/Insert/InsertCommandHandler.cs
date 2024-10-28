using AutoMapper;
using Byakkoder.Product.Application.Exceptions;
using Byakkoder.Product.Application.Interfaces;
using MediatR;

namespace Byakkoder.Product.Application.Products.Commands.Insert
{
    public class InsertCommandHandler : IRequestHandler<InsertCommand, Domain.Entities.Product>
    {
        #region Fields
        
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        
        public InsertCommandHandler(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        #endregion

        #region Handler Implementation

        public async Task<Domain.Entities.Product> Handle(InsertCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product product = await _productRepository.GetByName(request.Name);
            if (product != null)
            {
                throw new ItemExistsException("A product with the same name currently exists.");
            }

            product = _mapper.Map<Domain.Entities.Product>(request);

            await _productRepository.Create(product);

            return product;
        } 

        #endregion
    }
}
