using Week2.Entities;
using Week2.Tests;

namespace Week2
{
    internal class Program
    {
        static void Main()
        {
            return;
        }

        static void TestAll()
        {
            CustomerTests.Run();
            ProductTests.Run();
            OrderTests.Run();
        }
    }
}