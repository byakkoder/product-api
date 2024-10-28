using MediatR;

namespace Byakkoder.Product.Application.Products.Queries.GetById
{
    public class GetByIdQuery : IRequest<Domain.Entities.Product>
    {
        #region Properties
        
        public long Id { get; set; } 

        #endregion
    }
}
