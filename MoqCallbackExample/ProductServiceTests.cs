using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace MoqCallbackExample
{
    [TestClass]
    public class ProductServiceTests
    {
        [TestMethod]
        public void GetProducts_Creates_OrderSearchCriteria_Correctly()
        {
            const int customerId = 56789;
            const int orderId = 12345;

            OrderSearchCriteria orderSearchCriteria = new OrderSearchCriteria
            {
                OrderId = orderId
            };

            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock
                .Setup(m => m.GetOrder(orderSearchCriteria))
                .Returns(new Order());

            ProductService sut = new ProductService(orderRepositoryMock.Object);

            List<Product> result = sut.GetProducts(customerId, orderId);
        }

        [TestMethod]
        public void GetProducts_Creates_OrderSearchCriteria_Correctly_2()
        {
            const int customerId = 56789;
            const int orderId = 12345;

            OrderSearchCriteria recievedOrderSearchCriteria = null;

            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock
                .Setup(m => m.GetOrder(It.IsAny<OrderSearchCriteria>()))
                .Returns(new Order())
                .Callback<OrderSearchCriteria>(o => recievedOrderSearchCriteria = o);

            ProductService sut = new ProductService(orderRepositoryMock.Object);

            List<Product> result = sut.GetProducts(customerId, orderId);

            Assert.IsNotNull(recievedOrderSearchCriteria);
            Assert.AreEqual(orderId, recievedOrderSearchCriteria.OrderId);
        }

        [TestMethod]
        public void GetProducts_With_Archieved_Orders()
        {
            const int orderId = 12345;

            int receivedOrderId = 0;
            bool receivedArchieved = false;

            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock
                .Setup(m => m.GetOrder(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new Order())
                .Callback<int, bool>((o, a) =>
                {
                    receivedOrderId = o;
                    receivedArchieved = a;
                });

            ProductService sut = new ProductService(orderRepositoryMock.Object);

            List<Product> result = sut.GetProducts(orderId);

            Assert.AreEqual(orderId, receivedOrderId);
            Assert.AreEqual(true, receivedArchieved);
        }

        [TestMethod]
        public void GetProducts_Creates_OrderSearchCriteria_Correctly_2_Lamda()
        {
            const int customerId = 56789;
            const int orderId = 12345;

            OrderSearchCriteria recievedOrderSearchCriteria = null;

            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock
                .Setup(m => m.GetOrder(It.IsAny<OrderSearchCriteria>()))
                .Returns(new Order())
                .Callback((OrderSearchCriteria o) => recievedOrderSearchCriteria = o);

            ProductService sut = new ProductService(orderRepositoryMock.Object);

            List<Product> result = sut.GetProducts(customerId, orderId);

            Assert.IsNotNull(recievedOrderSearchCriteria);
            Assert.AreEqual(orderId, recievedOrderSearchCriteria.OrderId);
        }

        [TestMethod]
        public void GetProducts_With_Archieved_Orders_Lamda()
        {
            const int orderId = 12345;

            int receivedOrderId = 0;
            bool receivedArchieved = false;

            Mock<IOrderRepository> orderRepositoryMock = new Mock<IOrderRepository>();
            orderRepositoryMock
                .Setup(m => m.GetOrder(It.IsAny<int>(), It.IsAny<bool>()))
                .Returns(new Order())
                .Callback((int o, bool a) =>
                {
                    receivedOrderId = o;
                    receivedArchieved = a;
                });

            ProductService sut = new ProductService(orderRepositoryMock.Object);

            List<Product> result = sut.GetProducts(orderId);

            Assert.AreEqual(orderId, receivedOrderId);
            Assert.AreEqual(true, receivedArchieved);
        }
    }
}