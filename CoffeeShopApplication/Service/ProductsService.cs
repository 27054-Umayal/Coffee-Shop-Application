using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Repository;

namespace CoffeeShopApplication.Service
{
    public class ProductsService
    {
        private readonly ProductsRepo _productsRepo;

        public ProductsService(ProductsRepo productsRepo)
        {
            this._productsRepo = productsRepo;
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return this._productsRepo.GetProducts();
        }

        public Product? GetProduct(int productId)
        {
            return this._productsRepo.GetProduct(productId);
        }

        public bool IsValidProduct(int productId)
        {
            return this._productsRepo.GetProduct(productId) != null;
        }
    }
}
