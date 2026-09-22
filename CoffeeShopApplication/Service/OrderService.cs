namespace CoffeeShopApplication.Service
{
    public class OrderService
    {
        public OrderService()
        {

        }

        public IReadOnlyList GetOrders()
        {
            this._OrdersRepo.GetOrders();
        }

        public bool IsValidOrder(int orderId)
        {
            List <Orders> orders = this.GetOrders();
            return orderId > 0 && orderId < orders.Count;
        }
    }
}
