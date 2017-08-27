using Application.Domain;
using Moq;
using NUnit.Framework;

namespace Application.Tests.ProductService_Tests
{
    [TestFixture]
    public class When_creating_a_product
    {
        //[Test]
        //public void Then_repository_save_should_be_called()
        //{
        //    //Arrange
        //    var mockProductRepository = new Mock<IProductRepository>();
        //    mockProductRepository.Setup(p => p.Save(It.IsAny<Product>()));
        //    ProductService productService = new ProductService(mockProductRepository.Object);

        //    //Act
        //    productService.Create(new ProductViewModel());

        //    //Assert
        //    mockProductRepository.VerifyAll();

        //}

        [Test]
        public void An_exception_should_be_thrown_if_id_is_not_created()
        {
            //Arrange
            ProductViewModel productViewModel = new ProductViewModel() { Description = "Nice product", Name = "ProductA" };

            Mock<IProductRepository> mockProductRepository = new Mock<IProductRepository>();
            Mock<IProductIdBuilder> mockIdBuilder = new Mock<IProductIdBuilder>();
            mockIdBuilder.Setup(i => i.BuildProductIdentifier()).Returns(new ProductIdentifier());
            ProductService productService = new ProductService(mockProductRepository.Object
                , mockIdBuilder.Object);

            //Act
            productService.Create(productViewModel);

            //Assert
            mockProductRepository.Verify(p => p.Save(It.IsAny<Product>()));

            //------------------------
        }
    }
}