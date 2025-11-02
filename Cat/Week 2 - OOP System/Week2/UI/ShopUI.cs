using Week2.Data;
using Week2.Entities.Customers;
using Week2.Entities.Order;
using Week2.Entities.Payment;
using Week2.Entities.Stock;
using Week2.Entities.Transactions;
using Week2.Utils;

namespace Week2.UI
{
    internal class ShopUI : IMenuUI
    {
        public static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Shop ===");

                if (AppData.Customers.Count == 0)
                {
                    Console.WriteLine($"No customers found. Register customers before shopping.");
                    InputHelper.WaitForBack();
                    return;
                }

                Console.WriteLine(AppData.Customers);
                Console.WriteLine();

                int customerId = InputHelper.GetInt("Enter your id (-1 to cancel)");

                if (customerId == -1)
                {
                    return;
                }

                Customer customer;
                try
                {
                    customer = AppData.Customers.Find(customerId);
                    CustomerMenu(customer);
                    return;
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"No customer found with ID {customerId}. Press Enter...");
                    Console.ReadLine();
                }
            }
        }

        public static void CustomerMenu(Customer customer)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Orders ===");
                Console.WriteLine("1. Pick Order");
                Console.WriteLine("2. Add Order");
                Console.WriteLine("0. Back");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        PickOrder(customer);
                        break;
                    case "2":
                        AddOrder(customer);
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

        public static void AddOrder(Customer customer)
        {
            Order order = new();

            customer.Orders.Add(order);
            Console.Clear();
            Console.WriteLine("== Success ==");
            Console.WriteLine("Order added successfully: " + order);
            InputHelper.WaitForBack();
        }

        public static void PickOrder(Customer customer)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Orders ===");

                var filteredOrders = customer.Orders.Items
                    .Where(order => order.Status == OrderStatus.New || order.Status == OrderStatus.Hold)
                    .ToList();

                if (filteredOrders.Count == 0)
                {
                    Console.WriteLine("No orders with New or Hold status found.");
                    InputHelper.WaitForBack();
                    return;
                }

                foreach (var ord in filteredOrders)
                {
                    Console.WriteLine(ord);
                }

                int orderNumber = InputHelper.GetInt($"Enter the number of the order you wish to select (-1 to cancel)");

                if (orderNumber == -1)
                {
                    return;
                }

                Order? order = filteredOrders.Find(ord => ord.OrderNumber == orderNumber);

                if (order is null)
                {
                    Console.WriteLine($"No order found with number {orderNumber}. Press Enter...");
                    Console.ReadLine();
                    continue;
                }

                OrderMenu(order);
            }
        }
        public static void OrderMenu(Order order)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Order #{order.OrderNumber} ===");
                Console.WriteLine(order);
                Console.WriteLine();
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Finalize Order");
                Console.WriteLine("0. Back");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddProductToOrder(order);
                        break;
                    case "2":
                        FinalizeOrder(order);
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

        private static void AddProductToOrder(Order order)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Stock ===");

                // Only show products with available stock
                var products = AppData.Stock.Items.Where(p => p.Number > 0).ToList();

                if (products.Count == 0)
                {
                    Console.WriteLine("No products in stock. Press Enter to go back...");
                    Console.ReadLine();
                    return;
                }

                foreach (var prod in products)
                {
                    Console.WriteLine(prod);
                }

                int productId = InputHelper.GetInt("Enter the Id of the product to add (-1 to cancel)");
                if (productId == -1) return;

                Product product;
                try
                {
                    product = products.First(p => p.Id == productId);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine($"No product found with Id {productId} or out of stock. Press Enter...");
                    Console.ReadLine();
                    continue;
                }

                int quantity = InputHelper.GetInt(
                    $"Enter quantity (Available: {product.Number})",
                    min: 1,
                    max: product.Number
                );

                // Check if product is already in the order
                OrderItem? existingItem = order.Items.FirstOrDefault(item => item.Product.Id == product.Id);

                if (existingItem != null)
                {
                    OrderItem updatedItem = existingItem + quantity;

                    if (quantity <= product.Number)
                    {
                        existingItem.UpdateQuantity(updatedItem.Quantity);
                        product.Number -= quantity;
                        Console.WriteLine("Updated: " + existingItem);
                    }
                    else
                    {
                        Console.WriteLine("Not enough stock to add this quantity. Press Enter...");
                        Console.ReadLine();
                        continue;
                    }
                }
                else
                {
                    if (quantity <= product.Number)
                    {
                        order.Add(new OrderItem(product, quantity));
                        product.Number -= quantity;
                        Console.WriteLine($"{quantity} x {product.Name} added to order.");
                    }
                    else
                    {
                        Console.WriteLine("Not enough stock to add this quantity. Press Enter...");
                        Console.ReadLine();
                        continue;
                    }
                }

                order.UpdateStatus(OrderStatus.Hold);
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }

        private static void FinalizeOrder(Order order)
        {
            Console.Clear();
            Console.WriteLine("=== Finalize Order ===");
            Console.WriteLine(order);
            Console.WriteLine();

            Console.WriteLine("Choose payment method:");
            Console.WriteLine("1. Cash");
            Console.WriteLine("2. Credit");
            Console.WriteLine("3. Check");
            Console.WriteLine("0. Cancel");

            string? choice = Console.ReadLine();
            Payment payment;

            switch (choice)
            {
                case "1": 
                    double cashAmount = InputHelper.GetDouble("Enter cash amount");
                    payment = new Cash { CashValue = cashAmount };
                    break;
                case "2":
                    string number = InputHelper.GetString("Enter card number");
                    string type = InputHelper.GetString("Enter card type");
                    DateTime expire = InputHelper.GetDate("Enter expiration date");
                    payment = new Credit { Number = number, Type = type, ExpireDate = expire };
                    break;
                case "3":
                    string name = InputHelper.GetString("Enter check name");
                    string bankId = InputHelper.GetString("Enter bank ID");
                    payment = new Check { Name = name, BankId = bankId };
                    break;
                case "0":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Press Enter...");
                    Console.ReadLine();
                    return;
            }

            payment.Amount = order.Total;
            order.UpdateStatus(OrderStatus.Paid);

            AppData.Transactions.Add(new Transaction()
            {
                Order = order,
                Payment = payment
            });

            Console.WriteLine("Order finalized successfully! Press Enter...");
            Console.ReadLine();
        }


    }
}