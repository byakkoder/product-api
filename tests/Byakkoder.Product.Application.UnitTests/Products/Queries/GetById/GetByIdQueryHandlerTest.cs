using Byakkoder.Product.Application.Exceptions;
using Byakkoder.Product.Application.Interfaces;
using Byakkoder.Product.Application.Products.Queries.GetById;
using Moq;

namespace Byakkoder.Product.Application.UnitTests.Products.Queries.GetById
{
    public class GetByIdQueryHandlerTest
    {
        #region Fields
        
        private readonly GetByIdQueryHandler _getByIdQueryHandler = null!;
        private readonly Mock<IProductRepository> _productRepository = null!;
        private readonly Mock<IDiscountApiService> _discountApiService = null!;

        #endregion

        #region Constructor

        public GetByIdQueryHandlerTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _discountApiService = new Mock<IDiscountApiService>();
            _getByIdQueryHandler = new GetByIdQueryHandler(_productRepository.Object, _discountApiService.Object);
        }

        #endregion

        #region Test Methods
        
        [Fact]
        public async void Handle_GetById_Successfull_Test()
        {
            #region Arrange

            GetByIdQuery getByIdQuery = new GetByIdQuery()
            {
                Id = 1
            };

            Domain.Entities.Product expectedProduct = new Domain.Entities.Product() { Id = 1, ProductId = "1" };

            _productRepository.Setup(pr => pr.GetById(getByIdQuery.Id)).ReturnsAsync(expectedProduct);
            _discountApiService.Setup(ds => ds.GetDiscountByProductId("1")).ReturnsAsync(new Models.DiscountDto { Id = 1, Discount = 20 });

            #endregion

            #region Act

            Domain.Entities.Product product = await _getByIdQueryHandler.Handle(getByIdQuery, default);

            #endregion

            #region Assert

            Assert.NotNull(product);
            Assert.Equal(expectedProduct, product);
            Assert.Equal(20, product.Discount);
            _discountApiService.Verify(ds => ds.GetDiscountByProductId(expectedProduct.ProductId), Times.Once);

            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Handle_GetByIdFailed_ProductNotFound_Test(long id)
        {
            #region Arrange

            GetByIdQuery getByIdQuery = new GetByIdQuery()
            {
                Id = id
            };

            #endregion

            #region Act and Assert

            Assert.ThrowsAsync<NotFoundException>(() => _getByIdQueryHandler.Handle(getByIdQuery, default));
            _discountApiService.Verify(ds => ds.GetDiscountByProductId(It.IsAny<string>()), Times.Never);

            #endregion
        } 

        #endregion
    }
}
