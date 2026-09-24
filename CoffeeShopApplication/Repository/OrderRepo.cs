using CoffeeShopApplication.Core.Interfaces;
using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Enums;

namespace CoffeeShopApplication.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly Queue<Orders> _waitingOrders;
        private readonly List<Orders> _completedOrders;

        private readonly List<Orders> _activeOrders;

        public OrderRepo()
        {
            this._waitingOrders = new Queue<Orders>();
            this._completedOrders = new List<Orders>();
            this._activeOrders = new List<Orders>();
        }

        public IReadOnlyList<Orders> GetWaitingOrders()
        {
            return this._waitingOrders.ToList();
        }

        public IReadOnlyList<Orders> GetCompletedOrders()
        {
            return this._completedOrders;
        }

        public IReadOnlyList<Orders> GetActiveOrders()
        {
            return this._activeOrders;
        }

        public void PlaceOrder(Orders order)
        {
            this._waitingOrders.Enqueue(order);
            this._activeOrders.Add(order);
        }

        public Orders? RemoveOrder()
        {
            if (this._waitingOrders.Count == 0)
            {
                return null;
            }

            return this._waitingOrders.Dequeue();
        }

        public Orders? PeekOrder()
        {
            if (this._waitingOrders.Count == 0)
            {
                return null;
            }

            return this._waitingOrders.Peek();
        }

        public void CompleteOrder(Orders order)
        {
            this._activeOrders.Remove(order);
            this._completedOrders.Add(order);
        }

        public void UpdateOrderStatus(Orders order, OrderStatus orderStatus)
        {
            order.OrderStatus = orderStatus;
        }
    }
}
