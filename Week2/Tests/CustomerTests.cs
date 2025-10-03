using Week2.Entities.Customers;

namespace Week2.Tests
{
    // NOTE: Not ACTUAL tests. Just semi-manual verification.
    public static class CustomerTests
    {
        public static void Run()
        {
            Customers customers = new();

            Console.WriteLine("=== CUSTOMER MANAGEMENT TEST ===\n");

            Person person1 = new()
            {
                Phone = "123-4567",
                BillingAddress = "123 Main St",
                FullName = "John Doe"
            };

            Person person2 = new()
            {
                Phone = "987-6543",
                BillingAddress = "456 Oak Ave",
                FullName = "Jane Smith"
            };

            Company company1 = new()
            {
                Phone = "555-1234",
                Location = "New York",
                CompanyName = "Tech Corp"
            };

            Company company2 = new()
            {
                Phone = "555-5678",
                Location = "Silicon Valley",
                CompanyName = "Startup Inc"
            };

            Console.WriteLine("Adding customers...");
            customers.Add(person1, person2, company1, company2);

            Console.WriteLine($"Total customers: {customers.Count}\n");

            Console.WriteLine("ALL CUSTOMERS:");
            foreach (var customer in customers.Items)
            {
                Console.WriteLine($"  {customer}");
            }
            Console.WriteLine();

            // Test Editing
            Console.WriteLine("EDITING PERSON:");
            Console.WriteLine($"Before: {customers.Find(person1.Id)}");
            var editedPerson = customers.EditCustomer(person1.Id, phone: "999-8888", billingAddress: "789 Updated St");
            Console.WriteLine($"After: {editedPerson}\n");

            Console.WriteLine("EDITING COMPANY:");
            Console.WriteLine($"Before: {customers.Find(company1.Id)}");
            var editedCompany = customers.EditCustomer(company1.Id, phone: "111-2222", companyName: "Tech Corp Updated");
            Console.WriteLine($"After: {editedCompany}\n");

            // Test counting
            Console.WriteLine($"Customer count after edits: {customers.Count}");

            // Test finding customer
            Console.WriteLine("\nFINDING CUSTOMER:");
            var foundCustomer = customers.Find(company2.Id);
            Console.WriteLine($"Found: {foundCustomer}");

            // Test deleting customer
            Console.WriteLine("\nDELETING CUSTOMER:");
            customers.Remove(person2.Id);
            Console.WriteLine($"Customer count after deletion: {customers.Count}");

            // Test error cases
            Console.WriteLine("\n=== ERROR TESTING ===");

            try
            {
                Console.WriteLine("Testing invalid customer ID...");
                customers.Find(9999); // Should throw
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Correctly caught: {ex.Message}");
            }

            try
            {
                Console.WriteLine("Testing edit on deleted customer...");
                customers.EditCustomer(person2.Id, phone: "000-0000"); // Should throw
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Correctly caught: {ex.Message}");
            }

            // Final state
            Console.WriteLine("\nFINAL CUSTOMER LIST:");
            foreach (var customer in customers.Items)
            {
                Console.WriteLine($"  {customer}");
            }

            Console.WriteLine($"\nFinal count: {customers.Count}");
            Console.WriteLine("=== TEST COMPLETE ===");
        }
    }
}