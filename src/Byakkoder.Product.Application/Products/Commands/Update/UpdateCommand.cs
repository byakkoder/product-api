using MediatR;

namespace Byakkoder.Product.Application.Products.Commands.Update
{
    public class UpdateCommand : IRequest<Unit>
    {
        #region Properties
        
        public long Id { get; set; }

        public string ProductId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool Status { get; set; }

        public long Stock { get; set; }

        public double Price { get; set; } 

        #endregion
    }
}
