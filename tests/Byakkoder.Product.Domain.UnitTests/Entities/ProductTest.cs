using Byakkoder.Product.Domain.Exceptions;
using System.Diagnostics;

namespace Byakkoder.Product.Domain.UnitTests.Entities
{
    public class ProductTest
    {
        #region Test Methods

        [Theory]
        [InlineData(-1)]
        [InlineData(-500)]
        public void Product_PriceLimitValidation_Test(double price)
        {
            #region Arrange Act and Assert

            Assert.Throws<PriceOutOfRangeException>(() => new Domain.Entities.Product { Price = price });

            #endregion
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        [InlineData(500)]
        public void Product_DiscountLimitValidation_Test(double discount)
        {
            #region Arrange Act and Assert

            Assert.Throws<DiscountOutOfRangeException>(() => new Domain.Entities.Product { Discount = discount });

            #endregion
        }

        [Theory]
        [InlineData(500, 10, 450)]
        [InlineData(1000, 15, 850)]
        [InlineData(8500, 50, 4250)]
        [InlineData(4500000, 15, 3825000)]
        public void Calculate_TotalPrice_Test(double price, double discount, double expectedFinalPrice)
        {
            #region Arrange

            Domain.Entities.Product product = new Domain.Entities.Product()
            {
                Price = price,
                Discount = discount
            };

            #endregion

            #region Act and Assert

            Assert.Equal(expectedFinalPrice, product.FinalPrice);

            #endregion
        }

        #endregion
    }
}
