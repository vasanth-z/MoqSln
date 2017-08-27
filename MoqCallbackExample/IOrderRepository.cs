namespace MoqCallbackExample
{
    public interface IOrderRepository
    {
        Order GetOrder(OrderSearchCriteria searchCriteria);

        Order GetOrder(int orderId, bool archieved);
    }
}