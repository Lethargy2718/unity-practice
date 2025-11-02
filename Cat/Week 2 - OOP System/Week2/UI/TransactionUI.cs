using Week2.Data;
using Week2.Utils;

namespace Week2.UI
{
    internal class TransactionUI : IMenuUI
    {
        public static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== Transactions ===");

                if (AppData.Transactions.Count == 0)
                {
                    Console.WriteLine($"No transactions found.");
                }
                else
                { 
                    foreach (var transaction in AppData.Transactions)
                    {
                        Console.WriteLine(transaction);
                    }
                }

                InputHelper.WaitForBack();
                return;
            }
        }
    }
}
