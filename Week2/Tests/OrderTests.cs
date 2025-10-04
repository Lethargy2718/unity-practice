using Week2.Entities.Order;
using Week2.Entities.Stock;

namespace Week2.Tests
{
    // NOTE: Not ACTUAL tests. Just semi-manual verification.
    public static class OrderTests
    {
        public static void Run()
        {
            Console.WriteLine("=== ORDER MANAGEMENT TEST ===\n");

            // Sample products
            Product product1 = new() { Name = "Laptop", Price = 1200, Number = 10 };
            Product product2 = new() { Name = "Mouse", Price = 25, Number = 50 };
            Product product3 = new() { Name = "Keyboard", Price = 75, Number = 20 };

            // Create order items
            OrderItem item1 = new(product1, 2);
            OrderItem item2 = new(product2, 5);
            OrderItem item3 = new(product3, 1);

            Console.WriteLine("Creating order and adding items...");
            Order order = new();
            order.Add(item1, item2, item3);

            Console.WriteLine($"Total items in order: {order.Count}");
            Console.WriteLine("ALL ORDER ITEMS:");
            foreach (var item in order.Items)
            {
                Console.WriteLine($"  {item.Id}: {item.Product.Name} x{item.Quantity} = ${item.SalePrice}");
            }
            Console.WriteLine($"Order total: ${order.Total}\n");

            // Test quantity updates
            Console.WriteLine("UPDATING QUANTITY:");
            Console.WriteLine($"Before: Item {item1.Id} = {item1.SalePrice}");
            item1.UpdateQuantity(3);
            Console.WriteLine($"After: Item {item1.Id} = {item1.SalePrice}\n");

            // Test operator overloads
            Console.WriteLine("TESTING OPERATOR OVERLOADS:");
            var item2Plus = item2 + 2;
            Console.WriteLine($"Item2 +2: Quantity={item2Plus.Quantity}, SalePrice={item2Plus.SalePrice}");
            var item3Minus = item3 - 1;
            Console.WriteLine($"Item3 -1: Quantity={item3Minus.Quantity}, SalePrice={item3Minus.SalePrice}\n");

            // Test order status update
            Console.WriteLine($"Current status: {order.Status}");
            order.UpdateStatus(OrderStatus.Paid);
            Console.WriteLine($"Updated status: {order.Status}\n");

            // Test finding item
            Console.WriteLine("FINDING ITEM BY ID:");
            var foundItem = order.Find(item1.Id);
            Console.WriteLine($"Found: {foundItem.Id} = {foundItem.SalePrice}\n");

            // Test removing item
            Console.WriteLine("REMOVING ITEM:");
            order.Remove(item2.Id);
            Console.WriteLine($"Items count after removal: {order.Count}\n");

            // Test error handling
            Console.WriteLine("=== ERROR TESTING ===");
            try
            {
                Console.WriteLine("Testing invalid item ID...");
                order.Find(9999); // Should throw
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Correctly caught: {ex.Message}");
            }

            try
            {
                Console.WriteLine("Testing update beyond stock...");
                item1.UpdateQuantity(1000); // Should fail
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Correctly caught: {ex.Message}");
            }

            // Final order state
            Console.WriteLine("\nFINAL ORDER STATE:");
            foreach (var item in order.Items)
            {
                Console.WriteLine($"  {item.Id}: {item.Product.Name} x{item.Quantity} = ${item.SalePrice}");
            }
            Console.WriteLine($"Final order total: ${order.Total}");
            Console.WriteLine($"Order status: {order.Status}");
            Console.WriteLine("=== TEST COMPLETE ===");
        }
    }
}
