using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Enums;

namespace CoffeeShopApplication.Core.Interfaces
{
    public class IOrderService
    {
        public IReadOnlyList<Orders> GetCompletedOrders();

        public IReadOnlyList<Orders> GetProcessingOrders();

        public bool IsValidOrder(int orderId);

        public void PlaceOrder(Orders order);

        public Orders? RemoveOrder();

        public Orders? PeekOrder();

        public void UpdateOrderStatus(Orders order, OrderStatus orderStatus);

        public void CompleteOrder(Orders order);

        public async Task PrepareOrderAsync(Orders order);

        public async Task StageAsync(int stage, Orders order);
    }
}