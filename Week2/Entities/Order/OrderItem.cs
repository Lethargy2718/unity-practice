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

        public bool UpdateQuantity(int newQuantity)
        {
            int toAdd = newQuantity - Quantity;
            if (toAdd <= Product.Number)
            {
                Quantity = newQuantity;
                Product.Number -= toAdd;
                return true;
            }
            else
            {
                Console.WriteLine($"Cannot update quantity: Attempting to take {toAdd} items out of {Product.Number} total items.");
                return false;
            }
        }

        public static OrderItem operator +(OrderItem original, int quantity)
        {
            OrderItem copy = new(original.Product, original.Quantity);

            if (!copy.UpdateQuantity(copy.Quantity + quantity))
            {
                throw new InvalidOperationException("Not enough stock.");
            }

            return copy;
        }

        public static OrderItem operator ++(OrderItem original)
        {
            return original + 1;
        }

        public static OrderItem operator -(OrderItem original, int quantity)
        {
            OrderItem copy = new(original.Product, original.Quantity);

            int newQuantity = copy.Quantity - quantity;
            if (newQuantity < 0)
            {
                throw new InvalidOperationException("Not enough quantity.");
            }

            copy.Quantity = Math.Max(0, newQuantity);
            return copy;
        }

        public static OrderItem operator --(OrderItem original)
        {
            return original - 1;
        }
    }
}
