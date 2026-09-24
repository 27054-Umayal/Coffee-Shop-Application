using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Core.Interfaces
{
    public interface IProductService
    {
        public IReadOnlyList<Product> GetProducts();
    }
}
