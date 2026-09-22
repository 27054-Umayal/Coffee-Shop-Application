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
                new Product { ProductId = 1, ProductName = "Coffee", ProductPreparationTime = 2 },
                new Product { ProductId = 2, ProductName = "Tea", ProductPreparationTime = 2 },
                new Product { ProductId = 3, ProductName = "Biscuit", ProductPreparationTime = 2 },
                new Product { ProductId = 1, ProductName = "Milk", ProductPreparationTime = 2 },
                new Product { ProductId = 1, ProductName = "Bread", ProductPreparationTime = 2 },
            };
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return this._products;
        }

        public Product? GetProduct(int productId)
        {
            return this._products.FirstOrDefault(p => p.ProductId == productId);
        }
    }
}
