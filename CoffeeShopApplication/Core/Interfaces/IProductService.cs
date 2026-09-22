using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Repository;

namespace CoffeeShopApplication.Core.Interfaces
{
    public class IProductService
    {
        public IReadOnlyList<Product> GetProducts();

        public Product? GetProduct(int productId);

        public bool IsValidProduct(int productId);
    }
}
