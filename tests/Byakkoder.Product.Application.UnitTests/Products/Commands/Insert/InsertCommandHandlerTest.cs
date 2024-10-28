using AutoMapper;
using Byakkoder.Product.Application.Exceptions;
using Byakkoder.Product.Application.Interfaces;
using Byakkoder.Product.Application.Products.Commands.Insert;
using Moq;

namespace Byakkoder.Product.Application.UnitTests.Products.Commands.Insert
{
    public class InsertCommandHandlerTest
    {
        #region Fields
        
        private readonly InsertCommandHandler _insertCommandHandler = null!;
        private readonly Mock<IProductRepository> _productRepository = null!;
        private readonly Mock<IMapper> _mapper = null!;

        #endregion

        #region Constructor
        
        public InsertCommandHandlerTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _mapper = new Mock<IMapper>();
            _insertCommandHandler = new InsertCommandHandler(_productRepository.Object, _mapper.Object);
        }

        #endregion

        #region Test Methods
        
        [Fact]
        public async void Handle_Insert_Successfull_Test()
        {
            #region Arrange

            InsertCommand command = BuildTestInsertCommand();

            _mapper.Setup(m => m.Map<Domain.Entities.Product>(command)).Returns(new Domain.Entities.Product());

            #endregion

            #region Act

            Domain.Entities.Product product = await _insertCommandHandler.Handle(command, default);

            #endregion

            #region Assert

            Assert.NotNull(product);
            _productRepository.Verify(pr => pr.Create(product), Times.Once);

            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Handle_InsertFailed_ProductExists_Test(long id)
        {
            #region Arrange

            InsertCommand command = BuildTestInsertCommand(id);

            _productRepository.Setup(pr => pr.GetByName(command.Name)).ReturnsAsync(new Domain.Entities.Product());

            #endregion

            #region Act and Assert

            Assert.ThrowsAsync<ItemExistsException>(() => _insertCommandHandler.Handle(command, default));
            _mapper.Verify(m => m.Map<Domain.Entities.Product>(command), Times.Never);
            _productRepository.Verify(pr => pr.Create(It.IsAny<Domain.Entities.Product>()), Times.Never);

            #endregion
        }

        #endregion

        #region Private Test Methods
        
        private InsertCommand BuildTestInsertCommand(long id = 1)
        {
            return new InsertCommand()
            {
                ProductId = id.ToString(),
                Name = "Super Gaming PC",
                Description = "Description",
                Status = true,
                Stock = 30,
                Price = 500
            };
        } 

        #endregion
    }
}