using CoffeeShopApplication.Core.Interfaces;
using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Repository;

namespace CoffeeShopApplication.Service
{
    public class ProductsService : IProductService
    {
        private readonly IProductRepo _productsRepo;

        public ProductsService(IProductRepo productsRepo)
        {
            this._productsRepo = productsRepo;
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return this._productsRepo.GetProducts();
        }
    }
}
