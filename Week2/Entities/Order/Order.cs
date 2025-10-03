using Week2.Entities.Repository;

namespace Week2.Entities.Order
{
    enum OrderStatus
    {
        New,
        Hold,
        Paid,
        Cancelled
    }

    internal class Order : Repository<OrderItem>
    {
        private static int nextId = 0;
        public int Id { get; private set; } = nextId++;
        private readonly static Random rng = new();
        public int OrderNumber { get; } = rng.Next(1, 100000);
        public DateTime Date { get; } = DateTime.Now;
        public double Total => Items.Sum(item => item.SalePrice);
        public OrderStatus Status { get; set; } = OrderStatus.New;

        protected override int GetId(OrderItem item) => item.Id;

        public void UpdateStatus(OrderStatus newStatus) => Status = newStatus;

        public override string ToString()
        {
            return $"Order Number: {OrderNumber} | Date: {Date} | Total: {Total} | Status: {Status}\n" + base.ToString();
        }

    }
}
