using AutoMapper;
using Byakkoder.Product.Api.Dto;
using Byakkoder.Product.Api.Interfaces;
using Byakkoder.Product.Application.Products.Commands.Insert;
using Byakkoder.Product.Application.Products.Commands.Update;
using Byakkoder.Product.Application.Products.Queries.GetById;
using MediatR;

namespace Byakkoder.Product.Api.Handlers
{
    public class ProductHandler : IProductHandler
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductHandler(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ProductDto> GetById(long id)
        {
            GetByIdQuery getByIdQuery = new GetByIdQuery() { Id = id };

            Application.Models.ProductDto product = await _mediator.Send(getByIdQuery);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> Insert(ProductDto productDto)
        {
            InsertCommand insertCommand = _mapper.Map<InsertCommand>(productDto);

            Domain.Entities.Product product = await _mediator.Send(insertCommand);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task Update(ProductDto productDto)
        {
            UpdateCommand updateCommand = _mapper.Map<UpdateCommand>(productDto);

            await _mediator.Send(updateCommand);
        }
    }
}
