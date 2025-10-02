using System.Xml.Linq;

namespace Week2.Entities
{
    internal class Customer
    {
        private static int nextId = 0;
        public int Id { get; set; }

        public required string Phone { get; set; }

        public Customer()
        {
            Id = nextId++;
        }

        public virtual void Edit(string? phone = null)
        {
            if (phone is not null) Phone = phone;
        }

        public override string ToString() => $"Id: {Id} | Phone: {Phone}";
    }
}
