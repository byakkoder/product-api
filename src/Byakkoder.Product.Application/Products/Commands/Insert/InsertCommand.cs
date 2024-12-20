﻿using MediatR;

namespace Byakkoder.Product.Application.Products.Commands.Insert
{
    public class InsertCommand : IRequest<Application.Models.ProductDto>
    {
        #region Properties
        
        public string ProductId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string StatusName { get; set; } = null!;

        public long Stock { get; set; }

        public double Price { get; set; } 

        #endregion
    }
}
