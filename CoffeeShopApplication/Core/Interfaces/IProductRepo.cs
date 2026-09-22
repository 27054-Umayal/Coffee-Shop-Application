using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Core.Interfaces
{
    public class IProductRepo
    {
        public IReadOnlyList<Product> GetProducts();

        public Product? GetProduct(int productId);
    }
}
