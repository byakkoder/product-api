using Byakkoder.Product.Domain.Exceptions;

namespace Byakkoder.Product.Domain.Entities
{
    public class Product
    {
        #region Fields
        
        private double _price = 0;
        private double _discount = 0;

        #endregion

        #region Properties

        public long Id { get; set; }

        public string ProductId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public bool Status { get; set; }

        public long Stock { get; set; }

        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value < 0)
                {
                    throw new PriceOutOfRangeException("The product price must be a not negative value.");
                }

                _price = value;
            }
        }

        public double Discount
        {
            get
            {
                return _discount;
            }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new DiscountOutOfRangeException("The product discount must be between 0 and 100.");
                }

                _discount = value;
            }
        }

        public double FinalPrice => CalculateTotalPrice();

        #endregion

        #region Private Methods
        
        private double CalculateTotalPrice()
        {
            return Price * (100D - Discount) / 100D;
        } 

        #endregion
    }
}
