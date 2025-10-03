using Week2.Data;
using Week2.Entities.Customers;
using Week2.Utils;

namespace Week2.UI
{
    internal class CustomerUI
    {
        public static void CustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Customers ===");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Edit Customer");
                Console.WriteLine("3. Delete Customer");
                Console.WriteLine("4. View Customers");
                Console.WriteLine("0. Back");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddCustomer();
                        break;
                    case "2":
                        EditCustomer();
                        break;
                    case "3":
                        DeleteCustomer();
                        break;
                    case "4":
                        ViewCustomers();
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

        public static void AddCustomer()
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

        public static void EditCustomer()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Edit Customer ===");

                if (AppData.Customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                    InputHelper.WaitForBack();
                    return;
                }

                Console.WriteLine(AppData.Customers);
                Console.WriteLine();

                int customerId = InputHelper.GetInt("Enter the id of the customer to edit (-1 to cancel)");

                if (customerId == -1)
                {
                    Console.WriteLine("Edit cancelled.");
                    InputHelper.WaitForBack();
                    return;
                }

                Customer customer;
                try
                {
                    customer = AppData.Customers.Find(customerId);
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

        public static void DeleteCustomer()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("== Delete Customer ==");

                if (AppData.Customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                    InputHelper.WaitForBack();
                    return;
                }

                Console.WriteLine(AppData.Customers);
                Console.WriteLine();

                int customerId = InputHelper.GetInt("Enter the id of the customer you wish to delete (-1 to cancel)");

                if (customerId == -1)
                {
                    Console.WriteLine("Delete cancelled.");
                    InputHelper.WaitForBack();
                    return;
                }
                else if (AppData.Customers.Remove(customerId))
                {
                    Console.WriteLine($"Successfully deleted customer with id {customerId}. Press enter...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"Customer with id {customerId} not found. Press enter...");
                    Console.ReadLine();
                }
            }
        }

        public static void ViewCustomers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Customers ===");

                if (AppData.Customers.Count == 0)
                {
                    Console.WriteLine("No customers found.");
                    InputHelper.WaitForBack();
                    return;
                }

                Console.WriteLine(AppData.Customers);
                InputHelper.WaitForBack();
                return;
            }
        }

        public static void AddPerson()
        {
            Person person = new()
            {
                Phone = InputHelper.GetString("Enter phone number"),
                BillingAddress = InputHelper.GetString("Enter billing address"),
                FullName = InputHelper.GetString("Enter full name"),
            };

            AppData.Customers.Add(person);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("== Success ==");
                Console.WriteLine("Customer added successfully: " + person);
                InputHelper.WaitForBack();
                return;
            }
        }

        public static void AddCompany()
        {
            Company company = new()
            {
                Phone = InputHelper.GetString("Enter phone number"),
                CompanyName = InputHelper.GetString("Enter company name"),
                Location = InputHelper.GetString("Enter company location"),
            };

            AppData.Customers.Add(company);

            Console.Clear();
            Console.WriteLine("== Success ==");
            Console.WriteLine("Customer added successfully: " + company);
            InputHelper.WaitForBack();
        }

        public static void Edit(Person person)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Edit Person ===");
                Console.WriteLine(person);
                Console.WriteLine();
                Console.WriteLine("1. Edit Phone");
                Console.WriteLine("2. Edit Billing Address");
                Console.WriteLine("3. Edit Full Name");
                Console.WriteLine("0. Back");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        string newPhone = InputHelper.GetString("Enter new phone number");
                        person.Edit(phone: newPhone);
                        break;
                    case "2":
                        string newBilling = InputHelper.GetString("Enter new billing address");
                        person.Edit(billingAddress: newBilling);
                        break;
                    case "3":
                        string newName = InputHelper.GetString("Enter new full name");
                        person.Edit(fullName: newName);
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

        public static void Edit(Company company)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Edit Company ===");
                Console.WriteLine(company);
                Console.WriteLine();
                Console.WriteLine("1. Edit Phone");
                Console.WriteLine("2. Edit Location");
                Console.WriteLine("3. Edit Company Name");
                Console.WriteLine("0. Back");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        string newPhone = InputHelper.GetString("Enter new phone number");
                        company.Edit(newPhone, company.Location, company.CompanyName);
                        break;
                    case "2":
                        string newLocation = InputHelper.GetString("Enter new location");
                        company.Edit(company.Phone, newLocation, company.CompanyName);
                        break;
                    case "3":
                        string newName = InputHelper.GetString("Enter new company name");
                        company.Edit(company.Phone, company.Location, newName);
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

    }
}
