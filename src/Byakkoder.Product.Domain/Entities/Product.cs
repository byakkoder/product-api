using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Byakkoder.Product.Domain.Entities
{
    public class Product
    {
        public long Id { get; set; }

        public string ProductId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool Status { get; set; }

        public long Stock { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double FinalPrice { get; set; }
    }
}
