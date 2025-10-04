using Week2.Entities.Transactions;
using Week2.Entities.Customers;
using Week2.Entities.Order;
using Week2.Entities.Stock;

namespace Week2.Data
{
    internal static class AppData
    {
        public static Stock Stock { get; } = new();
        public static Customers Customers { get; } = new();
        public static List<Transaction> Transactions { get; set; } = []; // Might switch to repo later. Might also move to customers instead.

        static AppData()
        {
            SeedCustomers();
            SeedStock();
            SeedOrders();
        }

        private static void SeedCustomers()
        {
            Customers.Add(
                new Person
                {
                    Phone = "555-1234",
                    FullName = "John Doe",
                    BillingAddress = "123 Main St"
                },
                new Person
                {
                    Phone = "555-5678",
                    FullName = "Jane Smith",
                    BillingAddress = "456 Elm Ave"
                },
                new Company
                {
                    Phone = "555-9999",
                    CompanyName = "TechCorp",
                    Location = "Silicon Valley"
                },
                new Company
                {
                    Phone = "555-0000",
                    CompanyName = "Foodies Inc",
                    Location = "New York"
                }
            );
        }

        private static void SeedStock()
        {
            Stock.Add(
                new Product
                {
                    Name = "Mechanical Keyboard",
                    Number = 15,
                    Description = "RGB backlit, clicky switches",
                    Price = 79.99
                },
                new Product
                {
                    Name = "Wireless Mouse",
                    Number = 30,
                    Description = "2.4GHz wireless mouse with silent clicks",
                    Price = 24.99
                },
                new Product
                {
                    Name = "HD Monitor",
                    Number = 10,
                    Description = "24-inch Full HD display",
                    Price = 129.99
                },
                new Product
                {
                    Name = "USB-C Cable",
                    Number = 50,
                    Description = "1m braided charging cable",
                    Price = 14.99
                },
                new Product
                {
                    Name = "Gaming Headset",
                    Number = 12,
                    Description = "Surround sound headset with mic",
                    Price = 59.99
                }
            );
        }

        private static void SeedOrders()
        {
            var keyboard = Stock.Items.First(p => p.Name == "Mechanical Keyboard");
            var mouse = Stock.Items.First(p => p.Name == "Wireless Mouse");
            var monitor = Stock.Items.First(p => p.Name == "HD Monitor");
            var usbCable = Stock.Items.First(p => p.Name == "USB-C Cable");
            var headset = Stock.Items.First(p => p.Name == "Gaming Headset");

            var johnDoe = Customers.Items.First(c =>
                c is Person person && person.FullName == "John Doe");
            var janeSmith = Customers.Items.First(c =>
                c is Person person && person.FullName == "Jane Smith");
            var techCorp = Customers.Items.First(c =>
                c is Company company && company.CompanyName == "TechCorp");

            var order1 = new Order();
            order1.Add(new OrderItem(keyboard, 1));
            order1.Add(new OrderItem(mouse, 2));
            order1.UpdateStatus(OrderStatus.Paid);
            johnDoe.Orders.Add(order1);

            var order2 = new Order();
            order2.Add(new OrderItem(monitor, 1));
            order2.Add(new OrderItem(usbCable, 3));
            order2.UpdateStatus(OrderStatus.Hold);
            janeSmith.Orders.Add(order2);

            var order3 = new Order();
            order3.Add(new OrderItem(keyboard, 5));
            order3.Add(new OrderItem(mouse, 10));
            order3.Add(new OrderItem(headset, 3));
            order3.UpdateStatus(OrderStatus.New);
            techCorp.Orders.Add(order3);

            var order4 = new Order();
            order4.Add(new OrderItem(headset, 1));
            order4.Add(new OrderItem(usbCable, 2));
            order4.UpdateStatus(OrderStatus.Cancelled);
            johnDoe.Orders.Add(order4);
        }
    }
}