using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Enums;

namespace CoffeeShopApplication.Core.Interfaces
{
    public class IOrderRepo
    {
        public IReadOnlyList<Orders> GetProcessingOrders();

        public IReadOnlyList<Orders> GetCompletedOrders();

        public void PlaceOrder(Orders order);

        public Orders? AssignOrder();

        public void CompleteOrder(Orders order);

        public void UpdateOrderStatus(Orders order, OrderStatus orderStatus);
    }
}
