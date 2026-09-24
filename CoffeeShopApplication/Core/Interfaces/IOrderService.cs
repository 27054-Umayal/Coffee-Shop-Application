using System.Globalization;
using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Enums;

namespace CoffeeShopApplication.Core.Interfaces
{
    public delegate void OrderStatusChangedHandler(string message);
 
    public delegate void OrderTimerChangedHandler(string message);

    public interface IOrderService
    {
        event OrderStatusChangedHandler? OrderStatusChanged;

        event OrderTimerChangedHandler? OrderTimerChanged;

        public IReadOnlyList<Orders> GetCompletedOrders();

        public IReadOnlyList<Orders> GetWaitingOrders();

        public IReadOnlyList<Orders> GetActiveOrders();

        public bool IsValidOrder(Guid orderId);

        public void PlaceOrder(Orders order);

        public Orders? RemoveOrder();

        public Orders? PeekOrder();

        public void UpdateOrderStatus(Orders order, OrderStatus orderStatus);

        public void CompleteOrder(Orders order);

        public Task PrepareOrderAsync(Orders order);

        public Task StageAsync(int stage, Orders order);
    }
}