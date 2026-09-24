using CoffeeShopApplication.Core.Interfaces;
using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Enums;

namespace CoffeeShopApplication.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;

        public event OrderStatusChangedHandler? OrderStatusChanged;

        public event OrderTimerChangedHandler? OrderTimerChanged;

        public OrderService(IOrderRepo orderRepo)
        {
            this._orderRepo = orderRepo;
        }

        public IReadOnlyList<Orders> GetCompletedOrders()
        {
            return this._orderRepo.GetCompletedOrders();
        }

        public IReadOnlyList<Orders> GetWaitingOrders()
        {
            return this._orderRepo.GetWaitingOrders();
        }

        public IReadOnlyList<Orders> GetActiveOrders()
        {
            return this._orderRepo.GetActiveOrders();
        }

        public void PlaceOrder(Orders order)
        {
            this._orderRepo.PlaceOrder(order);
            this.UpdateOrderStatus(order, OrderStatus.OrderPlaced);
        }

        public Orders? RemoveOrder()
        {
            return this._orderRepo.RemoveOrder();
        }

        public Orders? PeekOrder()
        {
            return this._orderRepo.PeekOrder();
        }

        public void UpdateOrderStatus(Orders order, OrderStatus orderStatus)
        {
            this._orderRepo.UpdateOrderStatus(order, orderStatus);
            this.OrderStatusChanged?.Invoke(
        $"Order {order.OrderId}: {orderStatus}");
        }

        public void CompleteOrder(Orders order)
        {
            this._orderRepo.CompleteOrder(order);
        }

        public async Task PrepareOrderAsync(Orders order)
        {
            this.UpdateOrderStatus(order, OrderStatus.Preparing);
            if (order.IsCancellationRequested())
            {
                this.CancelOrder(order);
                return;
            }

            await this.StageAsync(1, order);
            if (order.IsCancellationRequested())
            {
                this.CancelOrder(order);
                return;
            }

            await this.StageAsync(2, order);
            if (order.IsCancellationRequested())
            {
                this.CancelOrder(order);
                return;
            }

            Task stage3 = this.StageAsync(3, order);

            Task stage4 = this.StageAsync(4, order);

            await Task.WhenAll(stage3, stage4);
            if (order.IsCancellationRequested())
            {
                this.CancelOrder(order);
                return;
            }

            await this.StageAsync(5, order);
            if (order.IsCancellationRequested())
            {
                this.CancelOrder(order);
                return;
            }

            this.UpdateOrderStatus(order, OrderStatus.Completed);
        }

        public async Task StageAsync(int stage, Orders order)
        {
            await this.RunTimerAsync(3, stage, order);
            if (order.IsCancellationRequested())
            {
                return;
            }

            OrderStatus status = stage switch
            {
                1 => OrderStatus.S1Done,
                2 => OrderStatus.S2Done,
                3 => OrderStatus.S3Done,
                4 => OrderStatus.S4Done,
                5 => OrderStatus.S5Done,
                _ => throw new ArgumentOutOfRangeException(nameof(stage))
            };
            this.UpdateOrderStatus(order, status);
        }

        private async Task RunTimerAsync(int seconds, int stage, Orders order)
        {
            for (int remTime = seconds; remTime > 0; remTime--)
            {
                if (order.IsCancellationRequested())
                {
                    this.CancelOrder(order);
                    return;
                }

                this.OrderTimerChanged?.Invoke($"Order:{order.OrderId}:Stage:{stage}-{remTime}s remaining");
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        public bool IsValidOrder(Guid orderId)
        {
            IReadOnlyList<Orders> orders = this._orderRepo.GetWaitingOrders();
            return orders.FirstOrDefault(o => o.OrderId == orderId) != null;
        }

        public void CancelOrder(Orders order)
        {
            this.UpdateOrderStatus(order, OrderStatus.Cancelled);
            this._orderRepo.CancelOrder(order);
        }
    }
}
