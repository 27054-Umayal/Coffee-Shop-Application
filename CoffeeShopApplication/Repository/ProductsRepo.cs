using CoffeeShopApplication.Constants;
using CoffeeShopApplication.Core.Interfaces;
using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Repository
{
    public class ProductsRepo : IProductRepo
    {
        private readonly List<Product> _products;

        public ProductsRepo()
        {
            if (File.Exists(FileConstants.ProductFile))
            {
                this._products = FileOperation<Product>.ReadFromFile(FileConstants.ProductFile);
            }
            else
            {
                this._products = new List<Product>
                {
                    new Product { ProductId = Guid.NewGuid(), ProductName = "Coffee", ProductPreparationTime = 2 },
                    new Product { ProductId = Guid.NewGuid(), ProductName = "Tea", ProductPreparationTime = 2 },
                    new Product { ProductId = Guid.NewGuid(), ProductName = "Biscuit", ProductPreparationTime = 2 },
                    new Product { ProductId = Guid.NewGuid(), ProductName = "Milk", ProductPreparationTime = 2 },
                    new Product { ProductId = Guid.NewGuid(), ProductName = "Bread", ProductPreparationTime = 2 },
                };
                FileOperation<Product>.WriteToFile(this._products, FileConstants.ProductFile);
            }
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return FileOperation<Product>.ReadFromFile(FileConstants.ProductFile);
        }
    }
}
