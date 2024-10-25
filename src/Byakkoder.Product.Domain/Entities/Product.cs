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

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string StatusName { get; set; }

        public double Stock { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double FinalPrice { get; set; }
    }
}
