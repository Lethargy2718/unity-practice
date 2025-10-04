using Week2.Data;
using Week2.Entities.Customers;
using Week2.Entities.Repository;
using Week2.Utils;

namespace Week2.UI
{
    internal class CustomerUI : EntityUI<Customer>, IMenuUI
    {
        protected override string EntityName => "Customer";
        protected override Repository<Customer> EntityCollection => AppData.Customers;

        protected override void AddEntity()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Add Customer ===");
                Console.WriteLine("1. Person");
                Console.WriteLine("2. Company");
                Console.WriteLine("0. Back");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddPerson();
                        return;
                    case "2":
                        AddCompany();
                        return;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void AddPerson()
        {
            Person person = new()
            {
                Phone = InputHelper.GetString("Enter phone number"),
                BillingAddress = InputHelper.GetString("Enter billing address"),
                FullName = InputHelper.GetString("Enter full name"),
            };

            EntityCollection.Add(person);
            Console.Clear();
            Console.WriteLine("== Success ==");
            Console.WriteLine("Customer added successfully: " + person);
            InputHelper.WaitForBack();
        }

        private void AddCompany()
        {
            Company company = new()
            {
                Phone = InputHelper.GetString("Enter phone number"),
                CompanyName = InputHelper.GetString("Enter company name"),
                Location = InputHelper.GetString("Enter company location"),
            };

            EntityCollection.Add(company);
            Console.Clear();
            Console.WriteLine("== Success ==");
            Console.WriteLine("Customer added successfully: " + company);
            InputHelper.WaitForBack();
        }

        protected override void EditEntity()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("=== Edit Customer ===");

                if (EntityCollection.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                    InputHelper.WaitForBack();
                    return;
                }

                Console.WriteLine(EntityCollection);
                Console.WriteLine();

                int customerId = InputHelper.GetInt("Enter the id of the customer to edit (-1 to cancel)");

                if (customerId == -1)
                {
                    return;
                }

                Customer customer;
                try
                {
                    customer = EntityCollection.Find(customerId);
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"No customer found with ID {customerId}. Press Enter...");
                    Console.ReadLine();
                    continue;
                }

                Edit((dynamic)customer);
            }
        }

        private static void Edit(Person person)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Edit Person ===");
                Console.WriteLine(person);
                Console.WriteLine("1. Edit Phone");
                Console.WriteLine("2. Edit Billing Address");
                Console.WriteLine("3. Edit Full Name");
                Console.WriteLine("0. Back");
                Console.WriteLine();

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        person.Edit(phone: InputHelper.GetString("Enter new phone number"));
                        break;
                    case "2":
                        person.Edit(billingAddress: InputHelper.GetString("Enter new billing address"));
                        break;
                    case "3":
                        person.Edit(fullName: InputHelper.GetString("Enter new full name"));
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static void Edit(Company company)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Edit Company ===");
                Console.WriteLine(company);
                Console.WriteLine("1. Edit Phone");
                Console.WriteLine("2. Edit Location");
                Console.WriteLine("3. Edit Company Name");
                Console.WriteLine("0. Back");
                Console.WriteLine();

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        company.Edit(InputHelper.GetString("Enter new phone"), company.Location, company.CompanyName);
                        break;
                    case "2":
                        company.Edit(company.Phone, InputHelper.GetString("Enter new location"), company.CompanyName);
                        break;
                    case "3":
                        company.Edit(company.Phone, company.Location, InputHelper.GetString("Enter new company name"));
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press Enter...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        private static readonly CustomerUI _instance = new();
        public static void Menu() => _instance.ShowMenu();
    }
}
