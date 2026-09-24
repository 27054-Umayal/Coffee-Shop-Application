using CoffeeShopApplication.Core.Interfaces;
using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Repository
{
    public class ProductsRepo : IProductRepo
    {
        private readonly List<Product> _products;

        public ProductsRepo()
        {
            this._products = new List<Product>
            {
                new Product { ProductId = Guid.NewGuid(), ProductName = "Coffee", ProductPreparationTime = 2 },
                new Product { ProductId = Guid.NewGuid(), ProductName = "Tea", ProductPreparationTime = 2 },
                new Product { ProductId = Guid.NewGuid(), ProductName = "Biscuit", ProductPreparationTime = 2 },
                new Product { ProductId = Guid.NewGuid(), ProductName = "Milk", ProductPreparationTime = 2 },
                new Product { ProductId = Guid.NewGuid(), ProductName = "Bread", ProductPreparationTime = 2 },
            };
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return this._products;
        }
    }
}
