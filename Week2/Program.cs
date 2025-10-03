using Week2.Entities;
using Week2.Tests;
using Week2.UI;

namespace Week2
{
    internal class Program
    {
        static void Main()
        {
            MainMenu.Menu();
        }

        static void TestAll()
        {
            CustomerTests.Run();
            ProductTests.Run();
            OrderTests.Run();
        }
    }
}