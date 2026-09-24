using CoffeeShopApplication.Enums;

namespace CoffeeShopApplication.Core.Models
{
    public class Orders
    {
        private volatile bool _isCancellationRequested;

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public bool IsCancellationRequested()
        {
            return this._isCancellationRequested;
        }

        public void RequestCancellation()
        {
            this._isCancellationRequested = true;
        }
    }
}
