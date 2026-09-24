using CoffeeShopApplication.Core.Interfaces;
using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Enums;
using CoffeeShopApplication.View;

namespace CoffeeShopApplication.Controller
{
    public class CoffeeShopController
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productsService;
        private readonly IVendingMachineService _vendingMachineService;

        public CoffeeShopController(IOrderService orderService, IProductService productsService, IVendingMachineService vendingMachineService)
        {
            this._orderService = orderService;
            this._productsService = productsService;
            this._vendingMachineService = vendingMachineService;
            this._orderService.OrderStatusChanged += this.DisplayOrderStatus;
            this._orderService.OrderTimerChanged += this.DisplayOrderStatus;
        }

        public async Task RunMainMenu()
        {
            //MainMenu choice = ch switch
            //{
            //    PlaceOrder => this.PlaceOrder(),
            //    CancelOrder => this.CancelOrder(),
            //    Exit => ,
            //    _ => ApplicationConsole.DisplayMessage("Invalid Input entered"),
            //}

            // TODO: handle input
            MainMenu choice = default;
            do
            {
                ApplicationConsole.ViewMenu(typeof(MainMenu));
                bool isParsedIntInput = ApplicationConsole.ReadIntInput("Enter your choice:", out int intInput);
                if (!isParsedIntInput)
                {
                    ApplicationConsole.DisplayMessage("Invalid input. Please enter a number.");
                    continue;
                }

                if (!Enum.IsDefined(typeof(MainMenu), intInput))
                {
                    ApplicationConsole.DisplayMessage("Invalid choice. Please try again.");
                    continue;
                }

                choice = (MainMenu)intInput;
                switch (choice)
                {
                    case MainMenu.PlaceOrder:
                        this.PlaceOrder();
                        break;
                    case MainMenu.CancelOrder:
                        this.CancelOrder();
                        break;
                    case MainMenu.Exit:
                        ApplicationConsole.DisplayMessage("Exiting, Thankyou");
                        break;
                    case MainMenu.ViewActiveOrder:
                        this.ViewActiveOrders();
                        break;
                    case MainMenu.ViewWaitingForVMOrder:
                        this.ViewWaitingForVMOrders();
                        break;
                    case MainMenu.ViewOrderHistory:
                        this.ViewOrderHistory();
                        break;
                }
            }
            while (choice != MainMenu.Exit);
        }

        private void PlaceOrder()
        {
            IReadOnlyList<Product> products = this._productsService.GetProducts();
            for (int i = 0; i < products.Count; i++)
            {
                ApplicationConsole.DisplayMessage($"{i + 1}. {products[i].ProductName}");
            }

            int productNumber = this.ReadIdForOperation("Placing order");
            if (productNumber < 1 || productNumber > products.Count)
            {
                ApplicationConsole.DisplayMessage("Invalid product entered.");
                return;
            }

            Guid selectedProductId = this.GetGuidForProduct(productNumber);
            Orders order = new Orders { OrderId = Guid.NewGuid(), ProductId = selectedProductId, OrderStatus = OrderStatus.OrderPlaced };
            this._orderService.PlaceOrder(order);
            Orders? peekOrder = this._orderService.PeekOrder();
            if (peekOrder == null)
            {
                return;
            }

            bool vmAssigned = this._vendingMachineService.AssignVendingMachine(peekOrder.OrderId);
            if (!vmAssigned)
            {
                this._orderService.UpdateOrderStatus(peekOrder, OrderStatus.WaitForVm);
                return;
            }

            this._orderService.RemoveOrder();
            _ = this.ProcessOrderAsync(peekOrder);
        }

        private async Task ProcessOrderAsync(Orders order)
        {
            await this._orderService.PrepareOrderAsync(order);
            if (order.OrderStatus == OrderStatus.Completed)
            {
                this._orderService.CompleteOrder(order);
            }

            this._vendingMachineService.ReleaseVendingMachine(order.OrderId);
            this.ProcessWaitingOrder();
        }

        private void ProcessWaitingOrder()
        {
            Orders? waitingOrder = this._orderService.PeekOrder();
            if (waitingOrder == null)
            {
                return;
            }

            bool vmAssigned =
                this._vendingMachineService.AssignVendingMachine(waitingOrder.OrderId);
            if (!vmAssigned)
            {
                return;
            }

            this._orderService.RemoveOrder();
            _ = this.ProcessOrderAsync(waitingOrder);
        }

        private void CancelOrder()
        {
            IReadOnlyList<Orders> activeOrders = this._orderService.GetActiveOrders();
            if (activeOrders.Count == 0)
            {
                ApplicationConsole.DisplayMessage("No active orders...");
                return;
            }

            ApplicationConsole.DisplayOrders(activeOrders);

            int orderIdInput = this.ReadIdForOperation("cancelling order");
            if (orderIdInput < 1 || orderIdInput > activeOrders.Count)
            {
                ApplicationConsole.DisplayMessage("Invalid order selected.");
                return;
            }

            Guid selectedOrderId = this.GetGuidForOrder(orderIdInput);
            Orders? orderToBeCancelled = activeOrders.FirstOrDefault(order => order.OrderId == selectedOrderId);
            if (orderToBeCancelled == null)
            {
                ApplicationConsole.DisplayMessage("Invalid order.");
                return;
            }

            orderToBeCancelled.RequestCancellation();
            this._orderService.UpdateOrderStatus(orderToBeCancelled, OrderStatus.CancellationRequested);
        }

        private int ReadIdForOperation(string operation)
        {
            while (true)
            {
                bool isParsed = ApplicationConsole.ReadIntInput($"Enter the id for {operation}", out int idInput);
                if (isParsed)
                {
                    return idInput;
                }

                ApplicationConsole.DisplayMessage("Invalid input, please enter a number");
            }
        }

        private Guid GetGuidForProduct(int productId)
        {
            IReadOnlyList<Product> products = this._productsService.GetProducts();
            return products[productId - 1].ProductId;
        }

        private Guid GetGuidForOrder(int orderId)
        {
            IReadOnlyList<Orders> orders = this._orderService.GetActiveOrders();
            return orders[orderId - 1].OrderId;
        }

        private void ViewActiveOrders()
        {
            IReadOnlyList<Orders> activeOrders = this._orderService.GetActiveOrders();
            if (activeOrders.Count == 0)
            {
                ApplicationConsole.DisplayMessage("No active orders...");
                return;
            }

            ApplicationConsole.DisplayOrders(activeOrders);
        }

        private void ViewWaitingForVMOrders()
        {
            IReadOnlyList<Orders> waitingOrders = this._orderService.GetWaitingOrders();
            if (waitingOrders.Count == 0)
            {
                ApplicationConsole.DisplayMessage("No waiting orders...");
                return;
            }

            ApplicationConsole.DisplayOrders(waitingOrders);
        }

        private void ViewOrderHistory()
        {
            IReadOnlyList<Orders> orderHistory = this._orderService.GetOrderHistory();
            if (orderHistory.Count == 0)
            {
                ApplicationConsole.DisplayMessage("No orders in history...");
                return;
            }

            ApplicationConsole.DisplayOrders(orderHistory);
        }

        private void DisplayOrderStatus(string message)
        {
            ApplicationConsole.DisplayMessage(message);
        }

    }
}
