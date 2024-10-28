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

        #endregion

        #region Constructor
        
        public GetByIdQueryHandlerTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _getByIdQueryHandler = new GetByIdQueryHandler(_productRepository.Object);
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

            Domain.Entities.Product expectedProduct = new Domain.Entities.Product();

            _productRepository.Setup(pr => pr.GetById(getByIdQuery.Id)).ReturnsAsync(expectedProduct);

            #endregion

            #region Act

            Domain.Entities.Product product = await _getByIdQueryHandler.Handle(getByIdQuery, default);

            #endregion

            #region Assert

            Assert.NotNull(product);
            Assert.Equal(expectedProduct, product);

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

            #endregion
        } 

        #endregion
    }
}
