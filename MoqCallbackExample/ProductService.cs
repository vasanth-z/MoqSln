using System.Collections.Generic;

namespace MoqCallbackExample
{
    public class ProductService
    {
        private readonly IOrderRepository m_orderRepository;

        public ProductService(IOrderRepository orderRepository)
        {
            m_orderRepository = orderRepository;
        }

        public List<Product> GetProducts(int customerId, int orderId)
        {
            OrderSearchCriteria orderSearchCriteria = new OrderSearchCriteria
            {
                OrderId = customerId // THIS IS THE PROBLEM WE ARE GOING TO SEARCH FOR

                // Set some other search criteria...
            };

            Order retrievedOrder = m_orderRepository.GetOrder(orderSearchCriteria);

            // Do something else
            return retrievedOrder.Products;
        }

        public List<Product> GetProducts(int orderId)
        {
            Order retrievedOrder = m_orderRepository.GetOrder(orderId, true);

            return retrievedOrder.Products;
        }
    }
}