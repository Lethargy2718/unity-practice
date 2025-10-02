namespace Week2.Entities
{
    internal class Product
    {
        private const double MIN_PRICE = 10.0;
        private static int next_id = 0;
        public int Id { get; private set; }
        public int Number { get; set; }
        public required string Name { get; set; }

        public string Description { get; set; } = "";

        private double _price;
        public double Price
        {
            get => _price;
            set => _price = Math.Max(MIN_PRICE, value);
        }

        public Product()
        {
            Id = next_id++;
        }

        public void Edit(string? name = null, int? number = null, string? description = null, double? price = null)
        {
            if (name is not null) Name = name;
            if (number.HasValue) Number = number.Value;
            if (description is not null) Description = description;
            if (price.HasValue) Price = price.Value;
        }

        public override string ToString()
        {
            return $"Name: {Name} | Qty: {Number} | Description: {Description} | Price: {Price}";
        }
    }
}
