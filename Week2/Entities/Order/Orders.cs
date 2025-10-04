using Week2.Entities.Repository;

namespace Week2.Entities.Order
{
    internal class Orders : Repository<Order>
    {
        protected override int GetId(Order order) => order.OrderNumber;
    }
}
