using AutoMapper;
using Byakkoder.Product.Application.Exceptions;
using Byakkoder.Product.Application.Interfaces;
using Byakkoder.Product.Application.Products.Commands.Update;
using Moq;

namespace Byakkoder.Product.Application.UnitTests.Products.Commands.Update
{
    public class UpdateCommandHandlerTest
    {
        #region Fields
        
        private readonly UpdateCommandHandler _updateCommandHandler = null!;
        private readonly Mock<IProductRepository> _productRepository = null!;
        private readonly Mock<IProductStatusService> _productStatusService;
        private readonly Mock<IMapper> _mapper = null!;

        #endregion

        #region Constructor
        
        public UpdateCommandHandlerTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _productStatusService = new Mock<IProductStatusService>();
            _mapper = new Mock<IMapper>();
            _updateCommandHandler = new UpdateCommandHandler(_productRepository.Object, _productStatusService.Object, _mapper.Object);
        }

        #endregion

        #region Test Methods
        
        [Fact]
        public async void Handle_Update_Successfull_Test()
        {
            #region Arrange

            UpdateCommand command = BuildTestUpdateCommand();

            _productRepository.Setup(pr => pr.GetById(command.Id)).ReturnsAsync(new Domain.Entities.Product());
            _mapper.Setup(m => m.Map<Domain.Entities.Product>(command)).Returns(new Domain.Entities.Product());

            #endregion

            #region Act

            await _updateCommandHandler.Handle(command, default);

            #endregion

            #region Assert

            _productRepository.Verify(pr => pr.Update(It.IsAny<Domain.Entities.Product>()), Times.Once);

            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Handle_UpdateFailed_ProductNotFound_Test(long id)
        {
            #region Arrange

            UpdateCommand command = BuildTestUpdateCommand(id);

            #endregion

            #region Act and Assert

            Assert.ThrowsAsync<NotFoundException>(() => _updateCommandHandler.Handle(command, default));
            _mapper.Verify(m => m.Map<Domain.Entities.Product>(command), Times.Never);
            _productRepository.Verify(pr => pr.Create(It.IsAny<Domain.Entities.Product>()), Times.Never);

            #endregion
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Handle_UpdateFailed_ProductExists_Test(long id)
        {
            #region Arrange

            UpdateCommand command = BuildTestUpdateCommand(id);

            #endregion

            #region Act and Assert

            Assert.ThrowsAsync<ItemExistsException>(() => _updateCommandHandler.Handle(command, default));
            _mapper.Verify(m => m.Map<Domain.Entities.Product>(command), Times.Never);
            _productRepository.Verify(pr => pr.Create(It.IsAny<Domain.Entities.Product>()), Times.Never);

            #endregion
        }

        #endregion

        #region Private Test Methods
        
        private UpdateCommand BuildTestUpdateCommand(long id = 1)
        {
            return new UpdateCommand()
            {
                Id = id,
                ProductId = "1",
                Name = "Super Gaming PC",
                Description = "Description",
                StatusName = "Active",
                Stock = 30,
                Price = 500
            };
        } 

        #endregion
    }
}
