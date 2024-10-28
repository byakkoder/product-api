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
        private readonly IMapper _mapper;

        #endregion

        #region Constructor
        
        public UpdateCommandHandler(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
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
            if (product != null)
            {
                throw new ItemExistsException($"A product with the name {request.Name} currently exists.");
            }

            product = _mapper.Map<Domain.Entities.Product>(request);

            await _productRepository.Update(product);

            return Unit.Value;
        } 

        #endregion
    }
}
