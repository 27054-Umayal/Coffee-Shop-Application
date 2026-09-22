using CoffeeShopApplication.Core.Interfaces;
using CoffeeShopApplication.Core.Models;
using CoffeeShopApplication.Enums;
using CoffeeShopApplication.Service;
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
            MainMenu choice = MainMenu.PlaceOrder;
            do
            {
                switch (choice)
                {
                    case MainMenu.PlaceOrder:
                        await this.PlaceOrder();
                        break;
                    case MainMenu.CancelOrder:
                        break;
                    case MainMenu.Exit:
                        ApplicationConsole.DisplayMessage("Exiting, Thankyou");
                        break;
                }
            }
            while (choice != MainMenu.Exit);
        }

        private async Task PlaceOrder()
        {
            ApplicationConsole.ViewMenu(MainMenu);
            int id = this.ReadIdForOperation("Placing order");
            if (!this._productsService.IsValidProduct(id))
            {
                ApplicationConsole.DisplayMessage("Invalid product entered.");
                return;
            }

            Orders order = new Orders { OrderId = this.GenerateOrderId(), ProductId = id, OrderStatus = OrderStatus.OrderPlaced};
            this._orderService.PlaceOrder(order);
            ApplicationConsole.DisplayMessage("Order Placed");
            Orders? peekOrder = this._orderService.PeekOrder();
            if (peekOrder == null)
            {
                this._orderService.UpdateOrderStatus(order, OrderStatus.WaitForVm);
                ApplicationConsole.DisplayMessage("Waiting for vending machine");
                return;
            }

            bool vmAssigned = this._vendingMachineService.AssignVendingMachine(peekOrder.OrderId);
            if (!vmAssigned)
            {
                this._orderService.UpdateOrderStatus(order, OrderStatus.WaitForVm);
                ApplicationConsole.DisplayMessage("Waiting for vending machine");
                return;
            }

            this._orderService.RemoveOrder();
            await this._orderService.PrepareOrderAsync(peekOrder);
        }

        //private void CancelOrder()
        //{

        //}

        private int ReadIdForOperation(string operation)
        {
            //ApplicationConsole.DisplayMessage(operation);
            while (true)
            {
                if
            }
        }
    }
}
