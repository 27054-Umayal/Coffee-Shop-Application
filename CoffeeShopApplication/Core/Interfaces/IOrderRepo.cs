using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Enums;

namespace CoffeeShopApplication.Core.Interfaces
{
    public interface IOrderRepo
    {
        public IReadOnlyList<Orders> GetWaitingOrders();

        public IReadOnlyList<Orders> GetCompletedOrders();

        public IReadOnlyList<Orders> GetActiveOrders();

        public void PlaceOrder(Orders order);

        public Orders? RemoveOrder();

        public Orders? PeekOrder();

        public void CompleteOrder(Orders order);

        public void UpdateOrderStatus(Orders order, OrderStatus orderStatus);
    }
}
