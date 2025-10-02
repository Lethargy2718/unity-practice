using Week2.Entities.Repository;

namespace Week2.Entities
{
    internal class Stock : Repository<Product>
    {
        protected override int GetId(Product product) => product.Id;

        public Product EditProduct(int productId, string? name = null, int? number = null, string? description = null, double? price = null)
        {
            var product = Find(productId);
            product.Edit(name, number, description, price);
            return product;
        }
    }
}
