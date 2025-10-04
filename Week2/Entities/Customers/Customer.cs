using Week2.Entities.Order;

namespace Week2.Entities.Customers
{
    internal class Customer
    {
        private static int nextId = 0;
        public int Id { get; private set; } = nextId++;

        public Orders Orders { get; } = new();

        public required string Phone { get; set; }

        public virtual void Edit(string? phone = null)
        {
            if (phone is not null) Phone = phone;
        }

        public override string ToString() => $"Id: {Id} | Phone: {Phone}";
    }
}
