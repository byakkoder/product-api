using Byakkoder.Product.Application.Models;
using MediatR;

namespace Byakkoder.Product.Application.Products.Queries.GetById
{
    public class GetByIdQuery : IRequest<ProductDto>
    {
        #region Properties
        
        public long Id { get; set; } 

        #endregion
    }
}
