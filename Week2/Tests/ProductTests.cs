using Week2.Entities;

namespace Week2.Tests
{
    // NOTE: Not ACTUAL tests. Just semi-manual verification.
    public static class ProductTests
    {
        public static void Run()
        {
            Stock stock = new();

            Product p1 = new()
            {
                Name = "Laptop",
                Number = 5,
                Description = "Gaming laptop with RTX GPU",
                Price = 1200
            };

            Product p2 = new()
            {
                Name = "Something",
                Number = 10,
                Description = "A thing",
                Price = 5
            };

            stock.Add(p1, p2);

            Console.WriteLine(stock);
            Console.WriteLine("-------");

            stock.EditProduct(p1.Id, name: "EDITED!!!");
            stock.Remove(p2.Id);

            Console.WriteLine(stock);
            Console.WriteLine("Count: " + stock.Count);

            Console.WriteLine(stock.Find(p1.Id).Name);
        }
    }
}
