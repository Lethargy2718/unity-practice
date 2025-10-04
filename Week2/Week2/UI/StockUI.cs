using Week2.Data;
using Week2.Entities.Repository;
using Week2.Entities.Stock;
using Week2.Utils;

namespace Week2.UI
{
    internal class StockUI : EntityUI<Product>, IMenuUI
    {
        protected override string EntityName => "Product";
        protected override Repository<Product> EntityCollection => AppData.Stock;

        protected override void AddEntity()
        {
            Product product = new()
            {
                Name = InputHelper.GetString("Enter product name"),
                Description = InputHelper.GetString("Enter product description"),
                Number = InputHelper.GetInt("Enter product quantity"),
                Price = InputHelper.GetDouble("Enter product price"),
            };

            AppData.Stock.Add(product);
            Console.Clear();
            Console.WriteLine("== Success ==");
            Console.WriteLine("Product added successfully: " + product);
            InputHelper.WaitForBack();
        }

        protected override void EditEntity()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Edit Product ===");

                if (AppData.Stock.Count == 0)
                {
                    Console.WriteLine("No stock found.");
                    InputHelper.WaitForBack();
                    return;
                }

                Console.WriteLine(AppData.Stock);
                Console.WriteLine();

                int productId = InputHelper.GetInt("Enter the id of the product to edit (-1 to cancel)");

                if (productId == -1)
                {
                    return;
                }

                try
                {
                    Product product = AppData.Stock.Find(productId);
                    Edit(product);
                }
                catch (KeyNotFoundException)
                {
                    Console.WriteLine($"No customer found with ID {productId}. Press Enter...");
                    Console.ReadLine();
                }
            }
        }

        private static void Edit(Product product)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Edit Product ===");
                Console.WriteLine(product);
                Console.WriteLine();
                Console.WriteLine("1. Edit Name");
                Console.WriteLine("2. Edit Description");
                Console.WriteLine("3. Edit Quantity");
                Console.WriteLine("4. Edit Price");
                Console.WriteLine("0. Back");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        string newName = InputHelper.GetString("Enter new name");
                        product.Edit(name: newName);
                        break;
                    case "2":
                        string newDesc = InputHelper.GetString("Enter new description");
                        product.Edit(description: newDesc);
                        break;
                    case "3":
                        int newQty = InputHelper.GetInt("Enter new quantity", min: 0);
                        product.Edit(number: newQty);
                        break;
                    case "4":
                        double newPrice = InputHelper.GetDouble("Enter new price", min: 0);
                        product.Edit(price: newPrice);
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

        private static readonly StockUI _instance = new();
        public static void Menu() => _instance.ShowMenu();
    }
}
