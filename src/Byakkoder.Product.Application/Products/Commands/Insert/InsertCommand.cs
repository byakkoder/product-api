using MediatR;

namespace Byakkoder.Product.Application.Products.Commands.Insert
{
    public class InsertCommand : IRequest<Domain.Entities.Product>
    {
        #region Properties
        
        public string ProductId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool Status { get; set; }

        public long Stock { get; set; }

        public double Price { get; set; } 

        #endregion
    }
}
