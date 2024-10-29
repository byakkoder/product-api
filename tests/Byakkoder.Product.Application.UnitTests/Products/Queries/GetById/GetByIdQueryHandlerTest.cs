using AutoMapper;
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
        private readonly Mock<IProductStatusService> _productStatusService;
        private readonly Mock<IMapper> _mapper;

        #endregion

        #region Constructor

        public GetByIdQueryHandlerTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _discountApiService = new Mock<IDiscountApiService>();
            _productStatusService = new Mock<IProductStatusService>();
            _mapper = new Mock<IMapper>();
            _getByIdQueryHandler = new GetByIdQueryHandler(
                _productRepository.Object, _discountApiService.Object, _productStatusService.Object, _mapper.Object);
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
            _mapper.Setup(m=>m.Map<Application.Models.ProductDto>(expectedProduct)).Returns(new Models.ProductDto() { Id = 1, ProductId = "1" });
            _discountApiService.Setup(ds => ds.GetDiscountByProductId("1")).ReturnsAsync(new Models.DiscountDto { Id = 1, Discount = 20 });

            #endregion

            #region Act

            Application.Models.ProductDto product = await _getByIdQueryHandler.Handle(getByIdQuery, default);

            #endregion

            #region Assert

            Assert.NotNull(product);
            Assert.Equal(expectedProduct.ProductId, product.ProductId);
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
