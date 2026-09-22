using CoffeeShopApplication.Core.Models;

namespace CoffeeShopApplication.Repository
{
    public class OrderRepo
    {
        private readonly Queue<Orders> _orders;
        private readonly List<Orders> _completedOrders;

        public OrderRepo()
        {
            this._orders = new Queue<Orders>();
            this._completedOrders = new List<Orders>();
        }

        public IReadOnlyList GetOrders()
        {
            return this._orders.ToList();
        }

        public void PlaceOrder(Orders order)
        {
            this._orders.Enqueue(order);
        }

        public Orders? AssignOrder()
        {
            if (this._orders.Count == 0)
            {
                return null;
            }

            return this._orders.Dequeue();
        }

        public void CompleteOrder(Orders order)
        {
            this._completedOrders.Add(order);
        }

        public void UpdateOrderStatus(Orders order, OrderStatus orderStatus)
        {
            order.OrderStatus = orderStatus;
        }
    }
}
