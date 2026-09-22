using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Repository;

namespace CoffeeShopApplication.Service
{
    public class OrderService
    {
        private readonly OrderRepo _orderRepo;

        public OrderService(OrderRepo orderRepo)
        {
            this._orderRepo = orderRepo;
        }

        public IReadOnlyList<Orders> GetCompletedOrders()
        {
            return this._orderRepo.GetOrders();
        }

        public IReadOnlyList<Orders> GetProcessingOrders()
        {
            return this._orderRepo.GetProcessingOrders();
        }

        public bool IsValidOrder(int orderId)
        {
            IReadOnlyList<Orders> orders = this.GetProcessingOrders();
            return orderId > 0 && orderId <= orders.Count;
        }

        public void PlaceOrder(Orders order)
        {
            this._orderRepo.PlaceOrder(order);
        }

        public Orders? AssignOrder()
        {
            return this._orderRepo.AssignOrder();
        }

        public void UpdateOrderStatus(Orders order, OrderStatus orderStatus)
        {
            this._orderRepo.UpdateOrderStatus(order, orderStatus);
        }

        public void CompleteOrder(Orders order)
        {
            this._orderRepo.CompleteOrder(order);
        }

        public async Task PrepareOrderAsync(Orders order)
        {
            this._orderRepo.UpdateOrderStatus(order, OrderStatus.Preparing);
            await this.StageAsync(1, order);
            await this.StageAsync(2, order);
            Task stage3 = this.StageAsync(3, order);
            Task stage4 = this.StageAsync(4, order);
            await Task.WhenAll(stage3, stage4);
            await this.StageAsync(5, order);
            this._orderRepo.UpdateOrderStatus(order, OrderStatus.Completed);
        }

        public async Task StageAsync(int stage, Orders order)
        {
            await Task.Delay(TimeSpan.FromSeconds(30));
            OrderStatus status = stage switch
            {
                1 => OrderStatus.S1Done,
                2 => OrderStatus.S2Done,
                3 => OrderStatus.S3Done,
                4 => OrderStatus.S4Done,
                5 => OrderStatus.S5Done,
                _ => throw new ArgumentOutOfRangeException(nameof(stage))
            };
            this._orderRepo.UpdateOrderStatus(order, status);
        }
    }
}
