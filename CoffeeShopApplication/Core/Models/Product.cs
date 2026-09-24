namespace CoffeeShopApplication.Core.Models
{
    public class Product
    {
        public Guid ProductId { get; init; }

        public string ProductName { get; init; }

        public int ProductPreparationTime { get; init; }
    }
}
