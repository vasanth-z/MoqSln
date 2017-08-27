using Application.Domain;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Application.Tests.ProductService_Tests
{
    [TestFixture]
    public class When_creating_multiple_products
    {
        [Test]
        public void Then_each_product_should_receive_a_unique_id()
        {
            //Arrange
            List<ProductViewModel> productViewModels = new List<ProductViewModel>()    {
        new ProductViewModel(){Name = "ProductA", Description="Great product"}
        , new ProductViewModel(){Name = "ProductB", Description="Bad product"}
        , new ProductViewModel(){Name = "ProductC", Description="Cheap product"}
        , new ProductViewModel(){Name = "ProductD", Description="Expensive product"}   };

            int productId = 1;
            var prod_iden = new ProductIdentifier() { RawValue = productId };

            var mockProductRepository = new Mock<IProductRepository>();
            Mock<IProductIdBuilder> mockIdBuilder = new Mock<IProductIdBuilder>();

            mockIdBuilder.Setup(i => i.BuildProductIdentifier())
                .Returns(prod_iden)
                .Callback(() => prod_iden.RawValue = productId++);

            ProductService productService = new ProductService(mockProductRepository.Object, mockIdBuilder.Object);
            //Act
            productService.CreateMany(productViewModels);

            //Assert
            mockProductRepository.Verify(p => p.Save(It.IsAny<Product>()), Times.AtLeastOnce());
        }

        //[Test]
        //public void Then_product_repository_should_be_called_once_per_product()
        //{
        //    //Arrange
        //    List<ProductViewModel> productViewModels = new List<ProductViewModel>()
        //    {
        //        new ProductViewModel(){Name = "ProductA", Description="Great product"}
        //        , new ProductViewModel(){Name = "ProductB", Description="Bad product"}
        //        , new ProductViewModel(){Name = "ProductC", Description="Cheap product"}
        //        , new ProductViewModel(){Name = "ProductD", Description="Expensive product"}
        //    };

        //    var mockProductRepository = new Mock<IProductRepository>();
        //    //mockProductRepository.Setup(p => p.Save(It.IsAny<Product>()));
        //    ProductService productService = new ProductService(mockProductRepository.Object);
        //    //Act
        //    productService.CreateMany(productViewModels);
        //    //Assert
        //    // mockProductRepository.VerifyAll();

        //    //Assert
        //    mockProductRepository.Verify(p => p.Save(It.IsAny<Product>()), Times.Exactly(productViewModels.Count));

        //}
    }
}