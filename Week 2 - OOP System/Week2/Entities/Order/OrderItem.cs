using Week2.Entities.Stock;

namespace Week2.Entities.Order
{
    internal class OrderItem(Product product, int quantity)
    {
        private static int nextId = 0;
        public int Id { get; private set; } = nextId++;
        public Product Product { get; set; } = product;
        public int Quantity { get; set; } = quantity;
        public double SalePrice => Product.Price * Quantity;

        public void UpdateQuantity(int newQuantity)
        {
            Quantity = newQuantity;
        }

        public static OrderItem operator +(OrderItem original, int quantity)
        {
            return new(original.Product, original.Quantity + quantity);
        }

        public static OrderItem operator ++(OrderItem original)
        {
            return original + 1;
        }

        public static OrderItem operator -(OrderItem original, int quantity)
        {
            return new(original.Product, original.Quantity - quantity);
        }

        public static OrderItem operator --(OrderItem original)
        {
            return original - 1;
        }

        public override string ToString()
        {
            return $"{Product.Name} x {Quantity} - ${SalePrice:F2}";
        }
    }
}
