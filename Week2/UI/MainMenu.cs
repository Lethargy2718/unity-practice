namespace Week2.UI
{
    internal class MainMenu
    {
        public static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Main Menu ===");
                Console.WriteLine("1. Customers");
                Console.WriteLine("2. Stock");
                Console.WriteLine("3. Shop");
                Console.WriteLine("0. Exit");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CustomerUI.Menu();
                        break;
                    case "2":
                        StockUI.Menu();
                        break;
                    case "3":
                        ShopUI.ShopMenu();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue...");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
