using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Core.Interfaces
{
    public interface IProductRepo
    {
        public IReadOnlyList<Product> GetProducts();
    }
}
