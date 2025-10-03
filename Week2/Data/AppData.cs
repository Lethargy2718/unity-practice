using Week2.Entities.Stock;
using Week2.Entities.Customers;

namespace Week2.Data
{
    internal static class AppData
    {
        public static Stock Stock { get; } = new();
        public static Customers Customers { get; } = new();

        static AppData()
        {
            SeedCustomers();
            SeedStock();
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
    }
}
